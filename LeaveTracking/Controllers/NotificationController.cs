using LeaveTracking.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly NotificationService notificationService;
		public NotificationController(NotificationService notificationService)
		{
			this.notificationService = notificationService;
		}

		[HttpGet]
		[Route("GetUnreadNotifications")]
		public async Task<IActionResult> GetUnreadNotifications()
		{
			try
			{
				var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
				if (userIdClaim == null)
				{
					return Unauthorized("User ID claim not found.");
				}
				if (!int.TryParse(userIdClaim.Value, out int userId))
				{
					return BadRequest("Invalid user ID.");
				}
				var unreadNotifications = await notificationService.GetUnreadNotificationsAsync(userId);
				return Ok(unreadNotifications);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}
	}
}
