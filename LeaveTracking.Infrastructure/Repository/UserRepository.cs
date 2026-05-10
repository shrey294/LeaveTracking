using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LeaveTracking.Utility;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Infrastructure.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public UserRepository( LeaveTrackingDbContext context)
		{
			_context = context;
		}

		public async Task<AuthResponseDTO> Authenticate(UserTbl user)
		{
			try
			{
				var User = await _context.UserTbls.FirstOrDefaultAsync(x=>x.UserName ==  user.UserName);
				if (User == null) 
				{
					return new AuthResponseDTO
					{
						success = false,
						message = "User Not Found"
					};
				}
				if (!PasswordEncrypt.verifyPassword(user.Password, User.Password))
				{
					return new AuthResponseDTO
					{
						success = false,
						message = "UserName Or Password is incorrect"
					};
				}
				User.Token = CreateJwtToken(User);
				var newAccessToken = User.Token;
				var RefreshToken = CreaterefreshToken();
				User.RefreshToken = RefreshToken;
				User.ExpiryTime = DateTime.UtcNow.AddDays(1);
				await _context.SaveChangesAsync();
				return new AuthResponseDTO
				{
					success = true,
					AccessToken = newAccessToken,
					RefreshToken = RefreshToken
				};
			}
			catch (Exception)
			{
				throw;
			}
		}
		private string CreateJwtToken(UserTbl user)
		{
			var jwtTokenhandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("LeaveTrackingScereteKeySouth@@##803");
			var identity = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.Role,user.Role),
				new Claim(ClaimTypes.Name,$"{user.UserName}")
			});
			var Credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);
			var tokendescriptor = new SecurityTokenDescriptor
			{
				Subject = identity,
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = Credentials,
			};
			var token = jwtTokenhandler.CreateToken(tokendescriptor);
			return jwtTokenhandler.WriteToken(token);
		}
		private string CreaterefreshToken()
		{
			var tokenbytes = RandomNumberGenerator.GetBytes(64);
			var refreshtoken = Convert.ToBase64String(tokenbytes);
			var tokeninuser = _context.UserTbls.Any(x=>x.RefreshToken == refreshtoken);
			if (tokeninuser)
			{
				return CreaterefreshToken();
			}
			return refreshtoken;
		}
		public async Task<bool> RegisterUser(UserTbl userTbl)
		{
			try
			{
				//var userexists = await _context.UserTbls.FirstOrDefaultAsync(x => x.UserName.ToLower() == userTbl.UserName.ToLower() || x.Email.ToLower()==userTbl.Email.ToLower());

				//if (userexists != null) 
				//{
				//	return false;
				//}
				//userTbl.Password = PasswordEncrypt.HashPassword(userTbl.Password);
				//userTbl.Role = userTbl.Role;
				//userTbl.Token = "";
				//userTbl.DeptId = userTbl.DeptId;
				//await _context.AddAsync(userTbl);
				//await _context.SaveChangesAsync();
				//return true;

				var parameters = new[]
				{
					new SqlParameter("@UserName",SqlDbType.NVarChar){Value=userTbl.UserName},
					new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = userTbl.FirstName },
					new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = userTbl.LastName },
					new SqlParameter("@Email", SqlDbType.NVarChar) { Value = userTbl.Email },
					new SqlParameter("@Password", SqlDbType.NVarChar) { Value = userTbl.Password },  
					new SqlParameter("@Role", SqlDbType.NVarChar) { Value = userTbl.Role },
					new SqlParameter("@DeptId", SqlDbType.Int) { Value = userTbl.DeptId },
					new SqlParameter("@Manager_id", SqlDbType.Int) { Value = userTbl.ManagerId }
				};
				var result = await _context.Database.ExecuteSqlRawAsync("EXEC SP_register_new_user @UserName, @FirstName, @LastName, @Email, @Password, @Role, @DeptId, @Manager_id", parameters);
				return result > 0;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<AuthResponseDTO> refresh(TokenApiDto token)
		{
			try
			{
				string accesstoken = token.AccessToken;
				string refreshtoken = token.RefreshToken;
				var principal = GetPrincipalTokenFromExpiredToken(accesstoken);
				var username = principal.Identity.Name;
				var user = await _context.UserTbls.FirstOrDefaultAsync(x => x.UserName == username);
				if(user==null || user.RefreshToken!=refreshtoken || user.ExpiryTime <= DateTime.Now)
				{
					return new AuthResponseDTO
					{
						success = false,
						message = "Invalid Request"
					};
				}
				var newAccessToken = CreateJwtToken(user);
				var RefreshToken = CreaterefreshToken();
				user.RefreshToken = RefreshToken;
				await _context.SaveChangesAsync();
				return new AuthResponseDTO
				{
					AccessToken = newAccessToken,
					RefreshToken = RefreshToken,
					success = true,
				};
			}
			catch (Exception)
			{
				throw;
			}
		}
		private ClaimsPrincipal GetPrincipalTokenFromExpiredToken(string token)
		{
			try
			{
				var key = Encoding.ASCII.GetBytes("LeaveTrackingScereteKeySouth@@##803");
				var tokenvalidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateLifetime = false
				};
				var tokenHandler = new JwtSecurityTokenHandler();
				SecurityToken securityToken;
				var principal = tokenHandler.ValidateToken(token,tokenvalidationParameters, out securityToken);
				var jwtsecuritytoken = securityToken as JwtSecurityToken;
				if (jwtsecuritytoken == null || !jwtsecuritytoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase)) 
				{
					throw new SecurityTokenException("This is invalid Token");
				}
				return principal;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<ManagerListDTO>> ManagerList()
		{
			try
			{
				return await _context.UserTbls.Where(u=>u.Role== "Manager").Select(u=> new ManagerListDTO
				{
					managerid = u.UserId,
					name = u.FirstName+" "+u.LastName
				}).ToListAsync();
			}
			catch(Exception) 
			{
				throw;
			}
		}
	}
}
