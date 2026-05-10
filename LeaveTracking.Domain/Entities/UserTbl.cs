using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class UserTbl
{
    public int UserId { get; set; }

    public int? DeptId { get; set; }

    public int? ManagerId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiryTime { get; set; }

    public virtual DepartmentMaster? Dept { get; set; }
}
