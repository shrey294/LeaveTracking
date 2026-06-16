using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class LeaveHistoryDTO
	{
		public int? LeaveTypeId { get; set; }

		public int? UserId { get; set; }

		public DateTime? Date { get; set; }
		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string? Duration { get; set; }

		public string? Reason { get; set; }
	}
}
