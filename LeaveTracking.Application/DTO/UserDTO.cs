using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class UserDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Role { get; set; }
		public int? dept_id { get; set; }
	}
}
