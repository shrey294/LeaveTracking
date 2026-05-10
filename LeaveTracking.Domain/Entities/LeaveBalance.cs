using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class LeaveBalance
{
    public int LeaveId { get; set; }

    public int? UserId { get; set; }

    public int? DeptId { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? TotalLeaves { get; set; }

    public int? UsedLeaves { get; set; }

    public int? RemainingLeaves { get; set; }
}
