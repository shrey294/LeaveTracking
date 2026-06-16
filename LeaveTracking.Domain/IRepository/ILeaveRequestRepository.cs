using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface ILeaveRequestRepository
	{
		Task<IEnumerable<LeaveRequestDTO>> GetLeaveRequestsAsync();
		Task<int> GetUserIdByUsernameAsync(string username);
		Task<bool> AddLeaveRequest(LeaveHistory leaveHistory, int loggedInUserId, string username);
		Task<int> GetUserIdByUsername(string username);
	}
}
