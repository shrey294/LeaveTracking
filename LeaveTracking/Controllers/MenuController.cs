using LeaveTracking.Application.DTO;
using LeaveTracking.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class MenuController : ControllerBase
	{
		private readonly MenuService _menuservice;
		public MenuController(MenuService menuservice)
		{
			_menuservice = menuservice;
		}
		[Authorize]
		[HttpGet("GetRoleMenu")]
		public async Task<IActionResult> GetRoleMenuList(string role)
		{
			try
			{

				var result = await _menuservice.GetRoleMenuList(role);
				if (result == null)
				{
					return NotFound();
				}
				else
				{
					return Ok(result);
				}
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}
		[Authorize]
		[HttpGet("GetMenuList")]
		public async Task<IActionResult> GetRoleMenuNameList()
		{
			try
			{

				var result = await _menuservice.GetRoleMenuNameList();
				if (result == null)
				{
					return NotFound();
				}
				else
				{
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("AdminMenuList")]
		public async Task<IActionResult> AdminMenuList()
		{
			try
			{
				var result = await _menuservice.AdminMenuList();
				return Ok(result);
			}
			catch (Exception)
			{
				return BadRequest(new {success=false,message="Exception While Processing Request"});
			}
		}
		//[HttpPost("AddMenu")]
		//[Authorize(Roles ="Admin")]
		//public async Task<IActionResult> AddMenuRole(MenuDTO menuDTO)
		//{
		//	try
		//	{
		//		return Ok();
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}
		[HttpPost("AddMenuMapping")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddMenuRoleMapping(int menuid, string role)
		{
			try
			{
				var result = await _menuservice.AddMenuRoleMapping(menuid, role);
				if (result)
				{
					return Ok(new {success=true,message="mapping added successfully"});
				}
				else
				{
					return BadRequest(new { success = false, message = "Something Went Wrong While Inserting Record" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("EditMenuMapping")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditMenuRoleMapping(int permission_id, int menuid, string role)
		{
			try
			{
				var result = await _menuservice.EditMenuRoleMapping(permission_id, menuid, role);
				if (result)
				{
					return Ok(new { success = true, message = "mapping added successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Something Went Wrong While Inserting Record" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("DeleteMenuMapping")]
		public async Task<IActionResult> DeleteMenuMapping(int id)
		{
			try
			{
				var result = await _menuservice.DeleteMenuMapping(id);
				if (result)
				{
					return Ok(new { success = true, message = "Record deleted Successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "Record Not Found" });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong While Inserting Record" });
			}
		}
	}
}
