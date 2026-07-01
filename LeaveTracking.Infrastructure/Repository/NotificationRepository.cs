using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Infrastructure.Repository
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public NotificationRepository(LeaveTrackingDbContext context)
		{
			_context = context;
		}
		public async Task<List<NotificationTbl>> GetUnreadAsync(int user_id)
		{
			return await Task.FromResult(
				_context.NotificationTbls
					.Where(n => n.RecevierUserId == user_id && (n.IsRead == false || n.IsRead == null))
					.ToList());
		}

		public async Task<bool> markAllRead(int Recevier_user_id)
		{
			try
			{
				var notificationtomark = await _context.NotificationTbls.Where(x=>x.RecevierUserId==Recevier_user_id).ToListAsync();
				foreach (var notification in notificationtomark) 
				{
					notification.IsRead = true;
					notification.ReadDate = DateTime.Now;
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> MarkAsReadAsync(int notificationId)
		{
			try
			{
				var notification = await _context.NotificationTbls.FirstOrDefaultAsync(n => n.NotificationId == notificationId);
				if (notification == null)
				{
					return false;
				}
				notification.IsRead = true;
				notification.ReadDate = DateTime.Now;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
