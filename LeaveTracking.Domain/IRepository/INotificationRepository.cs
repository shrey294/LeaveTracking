using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface INotificationRepository
	{
		Task<List<NotificationTbl>> GetUnreadAsync(int user_id);
		Task<bool> MarkAsReadAsync(int notificationId);
		Task<bool> markAllRead(int Recevier_user_id);
	}
}
