using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracking.Domain.Entities;

[Table("User_tbl")]
public partial class UserTbl
{
    [Key]
    [Column("User_id")]
    public int UserId { get; set; }

    [Column("dept_id")]
    public int? DeptId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public string? Password { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Role { get; set; }

    [Column("First_Name")]
    [StringLength(150)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("Last_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiryTime { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("UserTbls")]
    public virtual DepartmentMaster? Dept { get; set; }
}
