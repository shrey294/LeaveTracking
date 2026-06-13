using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class MenuDTO
	{
		public int? MenuOrder { get; set; }

		public string? Label { get; set; }

		public string? Icon { get; set; }

		public string? Route { get; set; }

		public string? Role { get; set; }
	}
	
}
