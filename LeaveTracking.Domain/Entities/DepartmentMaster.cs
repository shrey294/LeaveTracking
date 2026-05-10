using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class DepartmentMaster
{
    public int DeptId { get; set; }

    public string? DepartmentName { get; set; }

    public string? DepartmentCode { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<UserTbl> UserTbls { get; set; } = new List<UserTbl>();
}
