using LeaveTracking.Application.Service;
using LeaveTracking.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin,Manager")]
	public class DepartmentController : ControllerBase
	{
		private readonly DepartmentService _service;

		public DepartmentController(DepartmentService service) 
		{
			_service = service;
		}
		[HttpGet("AllDepartment")]
		public async Task<IActionResult> GetDepartmentList()
		{
			try
			{
				var result = await _service.GetDepartmentList();
				return Ok(new {success=true,result});
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpGet("DepartmentID")]
		public async Task<IActionResult> Get_Department_ByID(int id)
		{
			try
			{
				var result = await _service.Get_Department_ByID(id);
				if (result == null) 
				{
					return BadRequest(new {success=false,message="Department Not Found"});
				}
				return Ok(result);
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("AddDepartment")]
		public async Task<IActionResult> Insert_Department(DepartmentDTO department)
		{
			try
			{
				var result = await _service.Insert_Department(department);
				if (result)
				{
					return Ok(new { success = true, result });
				}
				else
				{
					return BadRequest(new { success = false, message = "Error While Saving Department" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPut("UpdateDepartment")]
		public async Task<IActionResult> Update_Department(DepartmentDTO department)
		{
			try
			{
				var result = await _service.Update_Department(department);
				if (result)
				{
					return Ok(new { success = true, result });
				}
				else
				{
					return BadRequest(new { success = false, message = "Department Not Found" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpDelete("DeleteDepartment")]
		public async Task<IActionResult> Delete_Department(int id)
		{
			try
			{
				var result = await _service.Delete_Department(id);
				if (result)
				{
					return Ok(new { success = true, result });
				}
				else
				{
					return BadRequest(new { success = false, message = "Department Not Found" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
	}
}
