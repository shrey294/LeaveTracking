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
	public class RoleRepository:IRoleRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public RoleRepository(LeaveTrackingDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddRole(RoleTbl roleTbl)
		{
			try
			{
				await _context.RoleTbls.AddAsync(roleTbl);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<RoleTbl>> GetRoleList()
		{
			try
			{
				return await _context.RoleTbls.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<bool> EditRole(RoleTbl roleTbl)
		{
			try
			{
				var recordtoupdate= await _context.RoleTbls.FirstOrDefaultAsync(x=>x.RoleId==roleTbl.RoleId);
				if (recordtoupdate != null)
				{
					recordtoupdate.RoleName = roleTbl.RoleName;
					await _context.SaveChangesAsync();
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> DeleteRole(int role)
		{
			try
			{
				var recordtodelete = await _context.RoleTbls.FindAsync(role);
				if (recordtodelete != null) 
				{
					 _context.RoleTbls.Remove(recordtodelete);
					await _context.SaveChangesAsync();
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
