using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class UserService
	{
		private readonly IUserRepository _userrepo;
		public UserService(IUserRepository userrepo)
		{
			_userrepo = userrepo;
		}
		public async Task<bool> RegisterUser(UserDTO user)
		{
			var entity = new UserTbl
			{
				Email = user.Email,
				UserName = user.UserName,
				Password = user.Password,
				Role = user.Role,
				DeptId = user.dept_id
			};
			return await _userrepo.RegisterUser(entity);
		}
		public async Task<AuthResponseDTO> Authenticate(UserDTO user)
		{
			var entity = new UserTbl
			{
				UserName = user.UserName,
				Password = user.Password,
				Role = user.Role
			};
			return await _userrepo.Authenticate(entity);
		}
		public async Task<AuthResponseDTO> refresh(LeaveTracking.Application.DTO.TokenApiDto tokenApi)
		{
			var DTOMapping = new LeaveTracking.Domain.DTO.TokenApiDto
			{
				AccessToken = tokenApi.AccessToken,
				RefreshToken = tokenApi.RefreshToken
			};
			return await _userrepo.refresh(DTOMapping);
		}
	}
}
