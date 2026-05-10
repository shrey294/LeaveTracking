using LeaveTracking.Domain.Context;
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
	public class LeaveTypeRepository : ILeaveType
	{
		private readonly LeaveTrackingDbContext _context;
		public LeaveTypeRepository(LeaveTrackingDbContext context) 
		{
			_context = context;
		}

		public async Task<bool> AddLeaveType(LeaveType leave)
		{
			try
			{
				await _context.LeaveTypes.AddAsync(leave);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> DeleteLeaveType(int id)
		{
			try
			{
				var recordtodelete = await _context.LeaveTypes.FirstOrDefaultAsync(x=>x.LeaveId == id);
				if (recordtodelete != null)
				{
					recordtodelete.IsActive = false;
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception) 
			{
				throw;
			}
		}

		public async Task<bool> EditLeaveType(LeaveType leave)
		{
			try
			{
				var entityoupdate = await _context.LeaveTypes.FirstOrDefaultAsync(x=>x.LeaveId== leave.LeaveId);
				if (entityoupdate != null) 
				{
					entityoupdate.LeaveName = leave.LeaveName;
					entityoupdate.LeaveCode = leave.LeaveCode;
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<LeaveType>> leaveTypes()
		{
			return await _context.LeaveTypes.Where(x=>x.IsActive==true).ToListAsync();
		}
	}
}
