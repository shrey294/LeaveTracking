using LeaveTracking.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles=("Admin"))]
	public class RoleController : ControllerBase
	{
		private readonly RoleService roleService;
		public RoleController(RoleService roleService)
		{
			this.roleService = roleService;
		}
		[HttpGet("GetRole")]
		public async Task<IActionResult> GetRoleList()
		{
			try
			{
				var result = await roleService.GetRoleList();
				return Ok(result);
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("AddRole")]
		public async Task<IActionResult> AddRole(string rolename)
		{
			try
			{
				var result = await roleService.AddRole(rolename);
				if (result)
				{
					return Ok(new { success = true, message = "Role Add Successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Role Insert Fail" });
				}
			}
			catch (Exception) 
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			} 
		}
		[HttpPost("EditRole")]
		public async Task<IActionResult> EditRole(int roleid, string rolename)
		{
			try
			{
				var result = await roleService.EditRole(roleid,rolename);
				if (result)
				{
					return Ok(new { success = true, message = "Role updated Successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Role Update Fail" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("DeleteRole")]
		public async Task<IActionResult> DeleteRole(int role_id)
		{
			try
			{
				var result = await roleService.DeleteRole(role_id);
				if (result)
				{
					return Ok(new { success = true, message = "Role Delete Successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Role Not Found" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
	}
}
