using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class LeaveType
{
    public int LeaveId { get; set; }

    public string? LeaveName { get; set; }

    public string? LeaveCode { get; set; }

    public bool? IsActive { get; set; }
}
