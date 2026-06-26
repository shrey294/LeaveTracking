using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class NotificationService
	{
		private readonly INotificationRepository _notificationRepository;
		public NotificationService(INotificationRepository notificationRepository)
		{
			_notificationRepository = notificationRepository;
		}
		public async Task<List<NotificationTbl>> GetUnreadNotificationsAsync(int userId)
		{
			return await _notificationRepository.GetUnreadAsync(userId);
		}
	}
}
