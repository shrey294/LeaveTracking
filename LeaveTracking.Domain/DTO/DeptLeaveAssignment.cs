using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.DTO
{
	public class DeptLeaveAssignment
	{
		public int DeptId { get; set; }
		public string DeptName { get; set; }
		public int LeaveTypeId { get; set; }
		public string LeaveTypeName { get; set; }
		public int TotalLeaves { get; set; }
	}
}
