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
		public async Task<bool> MarkAsReadAsync(int notificationId)
		{
			return	await _notificationRepository.MarkAsReadAsync(notificationId);
		}
		public async Task<bool> markAllRead(int Recevier_user_id)
		{
			return await _notificationRepository.markAllRead(Recevier_user_id);
		}
	}
}
