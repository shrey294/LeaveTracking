using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface IUserRepository
	{
		Task<bool> RegisterUser(UserTbl userTbl, int loggedInUserId);
		Task<AuthResponseDTO> Authenticate(UserTbl user);
		Task<AuthResponseDTO> refresh(TokenApiDto token, string rolename);
		Task<List<ManagerListDTO>> ManagerList();
		Task<LeaveAssignmentResponse> Leave_assignment(LeaveBalance leaveBalance);
		Task<List<DeptLeaveAssignment>> GetDeptLeaveAssignments();
		Task<LeaveAssignmentResponse> Update_LeaveAssignment(LeaveBalance leaveBalance);
		Task<int> GetUserIdByUsername(string username);

	}
}
