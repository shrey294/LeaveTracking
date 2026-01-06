using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracking.Domain.Entities;

[Table("Department_master")]
public partial class DepartmentMaster
{
    [Key]
    [Column("Dept_id")]
    public int DeptId { get; set; }

    [Column("department_name")]
    [StringLength(50)]
    public string? DepartmentName { get; set; }

    [Column("department_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DepartmentCode { get; set; }

    [Column("location")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Location { get; set; }

    [InverseProperty("Dept")]
    public virtual ICollection<UserTbl> UserTbls { get; set; } = new List<UserTbl>();
}
