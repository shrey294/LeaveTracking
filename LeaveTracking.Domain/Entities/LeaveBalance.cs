using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LeaveTracking.Domain.Entities;

[Table("leave_balances")]
public partial class LeaveBalance
{
    [Key]
    [Column("leave_id")]
    public int LeaveId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("leave_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LeaveType { get; set; }

    [Column("total_leaves")]
    public int? TotalLeaves { get; set; }

    [Column("used_leaves")]
    public int? UsedLeaves { get; set; }

    [Column("remaining_leaves")]
    public int? RemainingLeaves { get; set; }
}
