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
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public DepartmentRepository(LeaveTrackingDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Delete_Department(int id)
		{
			try
			{
				var result = await _context.DepartmentMasters.FirstOrDefaultAsync(x=>x.DeptId == id);
				if (result == null)
				{
					return false;
				}
				else
				{
					 _context.DepartmentMasters.Remove(result);
					await _context.SaveChangesAsync();
					return true;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		public async Task<List<DepartmentDTO>> GetDepartmentList()
		{
			try
			{
				var result = await _context.DepartmentMasters.Select(d => new DepartmentDTO
				{
					Id = d.DeptId,
					DepartmentName = d.DepartmentName,
					DepartmentCode = d.DepartmentCode,
					location = d.Location
				}).ToListAsync();

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<DepartmentDTO> Get_Department_ByID(int id)
		{
			try
			{
				var result = await _context.DepartmentMasters.FirstOrDefaultAsync(x=>x.DeptId == id);
				return new DepartmentDTO
				{
					Id = id,
					DepartmentName = result.DepartmentName,
					DepartmentCode = result.DepartmentCode,
					location = result.Location
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> Insert_Department(DepartmentMaster department)
		{
			try
			{
				await _context.DepartmentMasters.AddAsync(department);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> Update_Department(DepartmentMaster departmentMaster)
		{
			try
			{
				var recordtoupdate = await _context.DepartmentMasters.FirstOrDefaultAsync(x => x.DeptId == departmentMaster.DeptId);
				if (recordtoupdate == null)
				{
					return false;
				}
				recordtoupdate.DepartmentName = departmentMaster.DepartmentName;
				recordtoupdate.DepartmentCode = departmentMaster.DepartmentCode;
				recordtoupdate.Location = departmentMaster.Location;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception) 
			{
				throw;
			}
		}
	}
}
