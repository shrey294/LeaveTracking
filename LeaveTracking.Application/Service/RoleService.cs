using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class RoleService
	{
		private readonly IRoleRepository _role;
		public RoleService(IRoleRepository role) 
		{
			_role = role;
		}
		public async Task<IEnumerable<RoleTbl>> GetRoleList()
		{
			return await _role.GetRoleList();
		}
		public async Task<bool> AddRole(string rolename)
		{
			var entity = new RoleTbl {
				RoleName = rolename,
			};
			return await _role.AddRole(entity);
		}
		public async Task<bool> EditRole(int roleid,string rolename)
		{
			var entity = new RoleTbl
			{
				RoleId = roleid,
				RoleName = rolename,
			};
			return await _role.EditRole(entity);
		}
		public async Task<bool> DeleteRole(int role)
		{
			return await _role.DeleteRole(role);
		}
	}
}
