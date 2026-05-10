using LeaveTracking.Application.DTO;
using LeaveTracking.Application.Service;
using LeaveTracking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class LeaveTypeController : ControllerBase
	{
		private readonly LeaveTypeService _service;
		public LeaveTypeController(LeaveTypeService service)
		{
			_service = service;
		}
		[HttpGet("GetLeaveList")]
		public async Task<IActionResult> LeaveTypeList()
		{
			try
			{
				var leavetypelist = await _service.leaveTypes();
				return Ok(leavetypelist);
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("AddLeaveType")]
		public async Task<IActionResult> AddLeaveType(LeaveTypeDTO leaveType)
		{
			try
			{
				var result = await _service.AddLeaveType(leaveType);
				if (result)
				{
					return Ok(new {success=true,message="Leave Type Add SuccessFully"});
				}
				else
				{
					return BadRequest(new { success = false, message = "Leave Type Add Failed" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("EditLeaveType")]
		public async Task<IActionResult> EditLeaveType(LeaveTypeDTO leaveType)
		{
			try
			{
				var result = await _service.EditLeaveType(leaveType);
				if (result)
				{
					return Ok(new { success = true, message = "Leave Type updated SuccessFully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Leave Type update Failed" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("DeleteLeave")]
		public async Task<IActionResult> DeleteLeave([FromBody] DeleteLeaveRequest request)
		{
			try
			{
				var result = await _service.DeleteLeaveType(request.Id);
				if (result)
				{
					return Ok(new { success = true, message = "Leave Type Deleted SuccessFully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Leave Type Delete Failed" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
	}
}
