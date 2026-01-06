using LeaveTracking.Application.DTO;
using LeaveTracking.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTracking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;
		public UserController(UserService userService) 
		{
			_userService = userService;
		}
		[Authorize(Roles="Admin")]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser(UserDTO user)
		{
			try
			{
				var result = await _userService.RegisterUser(user);
				if (result)
				{
					return Ok(new { success = true, message = "User registered Successfully" });
				}
				else
				{
					return BadRequest(new { success = false, message = "user with this creditails exists" });
				}
			}
			catch (Exception) 
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
		[HttpPost("Authenticate")]
		public async Task<IActionResult> Authenticate(UserDTO user)
		{
			try
			{
				var result = await _userService.Authenticate(user);
				if (result.success)
				{
					return Ok(new { result.AccessToken, result.RefreshToken });
				}
				else
				{
					return BadRequest(new { success = false, message = "UserName Or Password is incorrect" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new { success = false, message = ex.Message });
			}
		}
		[HttpPost("Refresh")]
		public async Task<IActionResult> refresh(TokenApiDto token)
		{
			try
			{
				var result = await _userService.refresh(token);
				if (result.success)
				{
					return Ok(new { result.AccessToken, result.RefreshToken });
				}
				else
				{
					return BadRequest(new { success = false, message = result.message });
				}
			}
			catch (Exception)
			{
				return BadRequest(new { success = false, message = "Something Went Wrong" });
			}
		}
	}
}
