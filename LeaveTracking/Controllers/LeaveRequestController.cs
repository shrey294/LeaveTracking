using LeaveTracking.Application.DTO;
using LeaveTracking.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class LeaveRequestController : ControllerBase
	{
		private readonly LeaveRequestService _leaveRequestService;
		private readonly LeaveTypeService _leaveTypeService;
		public LeaveRequestController(LeaveRequestService leaveRequestService, LeaveTypeService leaveTypeService)
		{
			_leaveRequestService = leaveRequestService;
			_leaveTypeService = leaveTypeService;
		}
		[HttpGet]
		public async Task<IActionResult> GetLeaveRequests()
		{
			try
			{
				// Logic to get leave requests
				var leaveRequests = await _leaveRequestService.GetLeaveRequestsAsync();
				if (leaveRequests == null)
				{
					return NotFound();
				}
				return Ok(leaveRequests);
			}
			catch (Exception ex)
			{
				// Handle the exception (e.g., log it)
				return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
			}
		}
		[HttpGet("GetLeaveList")]
		public async Task<IActionResult> LeaveTypeList()
		{
			try
			{
				var leavetypelist = await _leaveTypeService.leaveTypes();
				return Ok(leavetypelist);
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("AddLeaveRequest")]
		public async Task<IActionResult> AddLeaveRequest(LeaveHistoryDTO leaveHistory)
		{
			try
			{
				string username = User.Identity?.Name; // Get the username of the currently authenticated user
				leaveHistory.UserId = await _leaveRequestService.GetUserIdByUsername(username);
				var result = await _leaveRequestService.AddLeaveRequest(leaveHistory);
				if (result)
				{
					return Ok(new {success=true, message = "Leave request submitted successfully." });
				}
				else
				{
					return BadRequest(new {success=false, message = "Failed to Submit leave request." });
				}
			}
			catch (Exception ex)
			{
				// Handle the exception (e.g., log it)
				return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
			}
		}
	}
}
