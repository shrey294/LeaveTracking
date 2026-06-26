using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.DTO
{
	public class NotificationDto
	{
		public long Id { get; set; }
		public string? SenderUserName { get; set; }

		public string? Message { get; set; }

		public string? NotificationType { get; set; }

		public bool? IsRead { get; set; }

		public DateTime? CreatedDate { get; set; }

		public DateTime? ReadDate { get; set; }
	}
}
