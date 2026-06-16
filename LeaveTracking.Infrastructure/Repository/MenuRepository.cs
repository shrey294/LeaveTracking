using LeaveTracking.Domain.Context;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Infrastructure.Repository
{
	public class MenuRepository:IMenuRepository
	{
		private readonly LeaveTrackingDbContext _context;
		public MenuRepository(LeaveTrackingDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddMenuRole(MenuTbl menuTbl)
		{
			try
			{
				await _context.MenuTbls.AddAsync(menuTbl);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		public async Task<bool> AddMenuRoleMapping(int menuid, int role_id)
		{
			try
			{
				var entity = new RoleFormPermission
				{
					MenuId = menuid,
					RoleId = role_id
				};
				await _context.RoleFormPermissions.AddAsync(entity);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
			
		}

		public async Task<List<permissionmenulistDTO>> AdminMenuList()
		{
			try
			{
				var result = await (from rfp in _context.RoleFormPermissions
							  join mt in _context.MenuTbls
								  on rfp.MenuId equals mt.MenuId
							  join rt in _context.RoleTbls
							  on rfp.RoleId equals rt.RoleId
							  select new permissionmenulistDTO
							  {
								  permission_id = rfp.PermissionId,
								  Role = rt.RoleName,
								  Label = mt.Label
							  })
			  .ToListAsync();
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> DeleteMenuMapping(int id)
		{
			try
			{
				var menumapping = await _context.RoleFormPermissions.FindAsync(id);
				if (menumapping == null)
				{
					return false;
				}

				_context.RoleFormPermissions.Remove(menumapping);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> EditMenuRoleMapping(int permission_id,int menuid, int role)
		{
			try
			{
				var recordtoupdatd = await _context.RoleFormPermissions.FirstOrDefaultAsync(x=>x.PermissionId == permission_id);
				if (recordtoupdatd != null)
				{
					recordtoupdatd.RoleId = role;
					recordtoupdatd.MenuId = menuid;
					await _context.SaveChangesAsync();
				}
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<MenuTbl>> RoleMenuList(string role)
		{
			try
			{

				return await _context.MenuTbls.FromSqlInterpolated($"EXEC sp_get_menulist_by_role @Role={role}")
											  .AsNoTracking().ToListAsync();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		public async Task<List<MenuListDTO>> RoleMenuNameList()
		{
			return await _context.MenuTbls.Select(x => new MenuListDTO
			{
				MenuId = x.MenuId,
				Label = x.Label
			})
		.ToListAsync();
		}
	}
}
