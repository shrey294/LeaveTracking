using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using LeaveTracking.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class UserService
	{
		private readonly IUserRepository _userrepo;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserService(IUserRepository userrepo, IHttpContextAccessor _http)
		{
			_userrepo = userrepo;
			_httpContextAccessor = _http;
		}
		public async Task<bool> RegisterUser(UserDTO user)
		{
			string username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
			int loggedInUserId = await _userrepo.GetUserIdByUsername(username);

			var entity = new UserTbl
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				Password = PasswordEncrypt.HashPassword(user.Password),
				RoleId = user.Role_id,
				DeptId = user.deptid,
				ManagerId = user.managerid
			};
			return await _userrepo.RegisterUser(entity, loggedInUserId);
		}
		public async Task<AuthResponseDTO> Authenticate(UserDTO user)
		{

			var entity = new UserTbl
			{
				UserName = user.UserName,
				Password = user.Password,
				RoleId = user.Role_id
			};
			return await _userrepo.Authenticate(entity);
		}
		public async Task<AuthResponseDTO> refresh(LeaveTracking.Application.DTO.TokenApiDto tokenApi)
		{
			var DTOMapping = new LeaveTracking.Domain.DTO.TokenApiDto
			{
				AccessToken = tokenApi.AccessToken,
				RefreshToken = tokenApi.RefreshToken
			};
			string rolename = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
			return await _userrepo.refresh(DTOMapping, rolename);
		}
		public async Task<bool> ForgotPassword(string email)
		{
			var emailexisits = await _userrepo.CheckEmailExists(email);
			if (emailexisits)
			{
				return await _userrepo.ForgotPassword(email);

			}
			else
			{
				return false;
			}
		}
		public async Task<List<LeaveTracking.Domain.DTO.ManagerListDTO>> ManagerList()
		{
			return await _userrepo.ManagerList();
		}
		public async Task<LeaveAssignmentResponse> Leave_assignment(LeaveBalanceDTO leaveBalance)
		{
			var mapping = new LeaveBalance
			{
				DeptId = leaveBalance.DeptId,
				LeaveTypeId = leaveBalance.LeaveTypeId,
				TotalLeaves = leaveBalance.TotalLeaves,
				UsedLeaves = 0,
				RemainingLeaves = leaveBalance.TotalLeaves
			};

			return await _userrepo.Leave_assignment(mapping);
		}
		public async Task<List<DeptLeaveAssignment>> GetDeptLeaveAssignments()
		{
			return await _userrepo.GetDeptLeaveAssignments();
		}
		public async Task<LeaveAssignmentResponse> Update_LeaveAssignment(LeaveBalanceDTO leaveBalance)
		{
			var mapping = new LeaveBalance
			{
				DeptId = leaveBalance.DeptId,
				LeaveTypeId = leaveBalance.LeaveTypeId,
				TotalLeaves = leaveBalance.TotalLeaves,
				RemainingLeaves = leaveBalance.TotalLeaves
			};
			return await _userrepo.Update_LeaveAssignment(mapping);
		}
		public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
		{
			var result = await _userrepo.CheckEmailExists(resetPasswordDTO.Email);
			if (result)
			{
				return await _userrepo.checkTokenandexpiry(resetPasswordDTO.Email,resetPasswordDTO.EmailToken, resetPasswordDTO.NewPassword);
			}
			else
			{
				return false;
			}
		}
	}
}
