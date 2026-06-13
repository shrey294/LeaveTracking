using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using LeaveTracking.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class UserService
	{
		private readonly IUserRepository _userrepo;
		public UserService(IUserRepository userrepo)
		{
			_userrepo = userrepo;
		}
		public async Task<bool> RegisterUser(UserDTO user)
		{
			var entity = new UserTbl
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				Password = PasswordEncrypt.HashPassword(user.Password),
				Role = user.Role,
				DeptId = user.deptid,
				ManagerId = user.managerid
			};
			return await _userrepo.RegisterUser(entity);
		}
		public async Task<AuthResponseDTO> Authenticate(UserDTO user)
		{

			var entity = new UserTbl
			{
				UserName = user.UserName,
				Password = user.Password,
				Role = user.Role
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
			return await _userrepo.refresh(DTOMapping);
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
	}
}
