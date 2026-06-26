using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
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
	}
}
