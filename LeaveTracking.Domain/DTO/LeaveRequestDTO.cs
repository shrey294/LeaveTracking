using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.DTO
{
	public class LeaveRequestDTO
	{
		public string? LeaveTypeName { get; set; }

		public int? UserId { get; set; }

		public DateTime? Date { get; set; }

		public string? Duration { get; set; }

		public string? Reason { get; set; }

		public string? Status { get; set; }

		public int? ApprovedBy { get; set; }
	}
}
