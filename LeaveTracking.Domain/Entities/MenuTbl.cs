using System;
using System.Collections.Generic;

namespace LeaveTracking.Domain.Entities;

public partial class MenuTbl
{
    public int MenuId { get; set; }

    public int? MenuOrder { get; set; }

    public string? Label { get; set; }

    public string? Icon { get; set; }

    public string? Route { get; set; }
}
