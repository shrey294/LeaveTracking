using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Infrastructure.Repository
{
	public class LeaveRequestRepository : ILeaveRequestRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public LeaveRequestRepository(LeaveTrackingDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddLeaveRequest(LeaveHistory leaveHistory)
		{
			try
			{
				await _context.LeaveHistories.AddAsync(leaveHistory);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<LeaveRequestDTO>> GetLeaveRequestsAsync()
		{
			try
			{
				return await _context.LeaveHistories
					.Join(_context.LeaveTypes,
						lh => lh.LeaveTypeId,
						lt => lt.LeaveId,
						(lh, lt) => new LeaveRequestDTO
						{
							LeaveTypeName = lt.LeaveName,
							UserId = lh.UserId,
							Date = lh.Date,
							Duration = lh.Duration,
							Reason = lh.Reason,
							Status = lh.Status,
							ApprovedBy = lh.ApprovedBy
						})
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetUserIdByUsernameAsync(string username)
		{
			try
			{
				var User = await _context.UserTbls.FirstOrDefaultAsync(x => x.UserName == username);
				return User.UserId;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
