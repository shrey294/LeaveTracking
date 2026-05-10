using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface IUserRepository
	{
		Task<bool> RegisterUser(UserTbl userTbl);
		Task<AuthResponseDTO> Authenticate(UserTbl user);
		Task<AuthResponseDTO> refresh(TokenApiDto token);
		Task<List<ManagerListDTO>> ManagerList();
	}
}
