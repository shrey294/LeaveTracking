using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class LeaveHistory
{
    public int LeaveHistroyId { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? UserId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Duration { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? InsertDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }
}
