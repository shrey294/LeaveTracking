using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class RoleFormPermission
{
    public int PermissionId { get; set; }

    public int? MenuId { get; set; }

    public int? RoleId { get; set; }
}
