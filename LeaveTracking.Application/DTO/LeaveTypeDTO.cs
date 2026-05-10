using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class LeaveTypeDTO
	{
		public int? id {  get; set; }
		public string? LeaveName { get; set; }

		public string? LeaveCode { get; set; }
	}
	public class DeleteLeaveRequest
	{
		public int Id { get; set; }
	}

}
