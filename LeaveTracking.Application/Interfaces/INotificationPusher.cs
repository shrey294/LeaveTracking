using LeaveTracking.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Interfaces
{
	public interface INotificationPusher
	{
		Task PushAsync(int recipientUserId, NotificationDto notification);
	}
}
