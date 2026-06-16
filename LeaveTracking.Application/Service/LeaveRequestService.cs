using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class LeaveRequestService
	{
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
		{
			_leaveRequestRepository = leaveRequestRepository;
		}
		public async Task<IEnumerable<LeaveRequestDTO>> GetLeaveRequestsAsync()
		{
			return await _leaveRequestRepository.GetLeaveRequestsAsync();
		}
		public async Task<int> GetUserIdByUsername(string username)
		{
			return await _leaveRequestRepository.GetUserIdByUsernameAsync(username);
		}

		public async Task<bool> AddLeaveRequest(LeaveHistoryDTO leaveHistory)
		{
			
			var leaveRequest = new LeaveHistory
			{
				LeaveTypeId = leaveHistory.LeaveTypeId,
				UserId = leaveHistory.UserId,
				Date = leaveHistory.Date,
				StartDate = leaveHistory.StartDate,
				EndDate = leaveHistory.EndDate,
				Duration = leaveHistory.Duration,
				Reason = leaveHistory.Reason,
				InsertDate = DateTime.Now,
			};
			return await _leaveRequestRepository.AddLeaveRequest(leaveRequest);
		}
	}
}
