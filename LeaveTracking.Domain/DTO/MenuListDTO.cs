using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.DTO
{
	public class MenuListDTO
	{
		public int MenuId { get; set; }
		public string? Label { get; set; }
	}
	public class permissionmenulistDTO
	{
		public int permission_id { get; set; }
		public string Role {  get; set; }
		public string Label { get; set; }
	}
}
