using LeaveTracking.Application.Interfaces;
using LeaveTracking.Application.Service;
using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.IRepository;
using LeaveTracking.Hubs;
using LeaveTracking.Infrastructure.Repository;
using LeaveTracking.Services;
using LeaveTracking.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LeaveTrackingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<ILeaveType, LeaveTypeRepository>();
builder.Services.AddScoped<LeaveTypeService>();
builder.Services.AddScoped<IMenuRepository,MenuRepository>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<LeaveRequestService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationPusher, SignalRNotificationPusher>();
builder.Services.AddSingleton<IUserIdProvider, NameIdentifierUserIdProvider>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LeaveTrackingScereteKeySouth@@##803")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
    };
    x.Events = new JwtBearerEvents
    {
       OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if(!string.IsNullOrEmpty(accessToken) &&
				path.StartsWithSegments("/hubs", StringComparison.OrdinalIgnoreCase))
            {
                context.Token = accessToken;
			}
            return Task.CompletedTask;
		}
    };
});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngular", policy =>
	{
		policy.WithOrigins("http://localhost:4200")  // ✅ Specific origin, NOT AllowAnyOrigin
			  .AllowAnyMethod()
			  .AllowAnyHeader()
			  .AllowCredentials();                   // ✅ Required for SignalR
	});
});
var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("AllowAngular");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/Hubs/NotificationHub");
app.Run();
