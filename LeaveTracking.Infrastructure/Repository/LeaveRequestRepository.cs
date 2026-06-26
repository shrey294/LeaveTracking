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
		public async Task<bool> AddLeaveRequest(LeaveHistory leaveHistory, int loggedInUserId, string username)
		{
			try
			{
				await _context.LeaveHistories.AddAsync(leaveHistory);
				await _context.SaveChangesAsync();
				int? receiver_user_id = await _context.UserTbls.Where(x=>x.UserId==loggedInUserId).Select(x=>x.ManagerId).FirstOrDefaultAsync();
				string message = $"New leave request from user {username}";

				var notification = new NotificationTbl
				{
					Message = message,
					NotificationType = "LeaveRequest",
					RecevierUserId = receiver_user_id,
					SenderUserId = loggedInUserId,
					SenderUserName = username,
					IsRead = false,
					CreatedDate = DateTime.UtcNow
				};
				await _context.NotificationTbls.AddAsync(notification);
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

		public Task<string> GetLeaveTypeNameById(int leaveTypeId)
		{
			try
			{
				var leaveType = _context.LeaveTypes.FirstOrDefault(x => x.LeaveId == leaveTypeId);
				if (leaveType != null)
				{
					return Task.FromResult(leaveType.LeaveName);
				}
				else
				{
					throw new Exception($"Leave type with ID {leaveTypeId} not found.");
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<int> GetmanagerId(int userId)
		{
			try 
			{
				int managerId = await _context.UserTbls.Where(x => x.UserId == userId).Select(x => x.ManagerId).FirstOrDefaultAsync()??0;
				return managerId;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<int> GetUserIdByUsername(string username)
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
