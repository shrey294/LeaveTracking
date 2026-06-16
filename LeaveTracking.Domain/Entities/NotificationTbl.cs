using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class NotificationTbl
{
    public long NotificationId { get; set; }

    public int? SenderUserId { get; set; }

    public int? RecevierUserId { get; set; }

    public string? Message { get; set; }

    public string? NotificationType { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ReadDate { get; set; }
}
