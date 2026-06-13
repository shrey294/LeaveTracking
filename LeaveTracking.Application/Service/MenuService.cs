using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class MenuService
	{
		private readonly IMenuRepository _menuRepository;
		public MenuService(IMenuRepository menuRepository) 
		{
			_menuRepository = menuRepository;
		}
		public async Task<List<MenuTbl>> GetRoleMenuList(string role)
		{
			return await _menuRepository.RoleMenuList(role);
		}
		public async Task<List<MenuListDTO>> GetRoleMenuNameList()
		{
			return await _menuRepository.RoleMenuNameList();
		}
		public async Task<bool> AddMenuRoleMapping(int menuid, string role)
		{
			return await _menuRepository.AddMenuRoleMapping(menuid, role);
		}
		public async Task<bool> EditMenuRoleMapping(int permission_id, int menuid, string role)
		{
			return await _menuRepository.EditMenuRoleMapping(permission_id, menuid, role);
		}
		public async Task<bool> AddMenuRole(MenuDTO menuDTO)
		{
			var entity = new MenuTbl
			{
				MenuOrder = menuDTO.MenuOrder,
				
				Route = menuDTO.Route,
				Icon = menuDTO.Icon,
				Label = menuDTO.Label
			};
			return await _menuRepository.AddMenuRole(entity);
		}
		public async Task<List<permissionmenulistDTO>> AdminMenuList()
		{
			return await _menuRepository.AdminMenuList();
		}
		public async Task<bool> DeleteMenuMapping(int id)
		{
			return await _menuRepository.DeleteMenuMapping(id);
		}
	}
}
