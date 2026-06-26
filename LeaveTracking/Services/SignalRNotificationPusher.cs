using LeaveTracking.Application.DTO;
using LeaveTracking.Application.Interfaces;
using LeaveTracking.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LeaveTracking.Services
{
	public class SignalRNotificationPusher : INotificationPusher
	{
		private readonly IHubContext<NotificationHub> _hubContext;
		public SignalRNotificationPusher(IHubContext<NotificationHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task PushAsync(int recipientUserId, NotificationDto notification)
		{
			
			await _hubContext.Clients.Group($"user-{recipientUserId}").SendAsync("ReceiveNotification", notification);
		}
	}
}
