using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface IRoleRepository
	{
		Task<IEnumerable<RoleTbl>> GetRoleList();
		Task<bool> AddRole(RoleTbl roleTbl);
		Task<bool> EditRole(RoleTbl roleTbl);
		Task<bool> DeleteRole(int role);
	}
}
