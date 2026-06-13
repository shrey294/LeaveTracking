using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class LeaveBalanceDTO
	{
		public int? DeptId { get; set; }

		public int? LeaveTypeId { get; set; }

		public int? TotalLeaves { get; set; }
	}
}
