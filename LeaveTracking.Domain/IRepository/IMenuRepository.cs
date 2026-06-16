using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface IMenuRepository
	{
		Task<List<MenuTbl>> RoleMenuList(string role);
		Task<List<MenuListDTO>> RoleMenuNameList();
		Task<List<permissionmenulistDTO>> AdminMenuList();
		Task<bool> DeleteMenuMapping(int id);
		Task<bool> AddMenuRoleMapping(int menuid, int role_id);
		Task<bool> EditMenuRoleMapping(int permission_id, int menuid, int role);
		Task<bool> AddMenuRole(MenuTbl menuTbl);
	}
}
