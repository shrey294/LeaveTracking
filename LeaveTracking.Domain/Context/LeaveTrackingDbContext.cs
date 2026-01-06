using System;
using System.Collections.Generic;
using LeaveTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracking.Domain.Context;

public partial class LeaveTrackingDbContext : DbContext
{
    public LeaveTrackingDbContext(DbContextOptions<LeaveTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveBalance>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__leave_ba__743350BCFAAF1C75");
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.HasOne(d => d.Dept).WithMany(p => p.UserTbls).HasConstraintName("FK_User_tbl_Department_master");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
