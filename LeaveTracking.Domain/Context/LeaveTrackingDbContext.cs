using System;
using System.Collections.Generic;
using LeaveTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracking.Domain.Context;

public partial class LeaveTrackingDbContext : DbContext
{
    public LeaveTrackingDbContext()
    {
    }

    public LeaveTrackingDbContext(DbContextOptions<LeaveTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }

    public virtual DbSet<LeaveHistory> LeaveHistories { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<MenuTbl> MenuTbls { get; set; }

    public virtual DbSet<RoleFormPermission> RoleFormPermissions { get; set; }

    public virtual DbSet<RoleTbl> RoleTbls { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-L48UG33\\SQLEXPRESS; Database=LeaveTracking;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.DeptId);

            entity.ToTable("Department_master");

            entity.Property(e => e.DeptId).HasColumnName("Dept_id");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_code");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("department_name");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location");
        });

        modelBuilder.Entity<LeaveBalance>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__leave_ba__743350BCFAAF1C75");

            entity.ToTable("leave_balances");

            entity.Property(e => e.LeaveId).HasColumnName("leave_id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_id");
            entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");
            entity.Property(e => e.RemainingLeaves).HasColumnName("remaining_leaves");
            entity.Property(e => e.TotalLeaves).HasColumnName("total_leaves");
            entity.Property(e => e.UsedLeaves).HasColumnName("used_leaves");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<LeaveHistory>(entity =>
        {
            entity.HasKey(e => e.LeaveHistroyId);

            entity.ToTable("Leave_History");

            entity.Property(e => e.LeaveHistroyId).HasColumnName("Leave_histroy_id");
            entity.Property(e => e.ApprovedBy).HasColumnName("Approved_by");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("delete_date");
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_date");
            entity.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasColumnName("insert_date");
            entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");
            entity.Property(e => e.Reason).IsUnicode(false);
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveId);

            entity.ToTable("Leave_Type");

            entity.Property(e => e.LeaveId).HasColumnName("leave_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LeaveCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Leave_code");
            entity.Property(e => e.LeaveName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Leave_name");
        });

        modelBuilder.Entity<MenuTbl>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("Menu_tbl");

            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("label");
            entity.Property(e => e.MenuOrder).HasColumnName("menu_order");
            entity.Property(e => e.Route)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("route");
        });

        modelBuilder.Entity<RoleFormPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId);

            entity.ToTable("Role_Form_Permission");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
        });

        modelBuilder.Entity<RoleTbl>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("Role_tbl");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("User_tbl");

            entity.HasIndex(e => e.UserName, "UQ_UserName").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.DeptId).HasColumnName("dept_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.ManagerId).HasColumnName("Manager_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.UserTbls)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK_User_tbl_Department_master");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
