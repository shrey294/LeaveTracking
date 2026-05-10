using LeaveTracking.Application.DTO;
using LeaveTracking.Domain.Entities;
using LeaveTracking.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Application.Service
{
	public class LeaveTypeService
	{
		private readonly ILeaveType _leavetype;
		public LeaveTypeService(ILeaveType leave)
		{
			_leavetype = leave;
		}
		public async Task<List<LeaveType>> leaveTypes()
		{
			return await _leavetype.leaveTypes();
		}
		public async Task<bool> AddLeaveType(LeaveTypeDTO leave)
		{
			var entity = new LeaveType
			{
				LeaveName = leave.LeaveName,
				LeaveCode = leave.LeaveCode,
				IsActive = true
			};
			var result = await _leavetype.AddLeaveType(entity);
			return result;
		}
		public async Task<bool> EditLeaveType(LeaveTypeDTO leave)
		{
			var entity = new LeaveType
			{
				LeaveId = leave.id??0,
				LeaveName = leave.LeaveName,
				LeaveCode = leave.LeaveCode,
				IsActive = true
			};
			var result = await _leavetype.EditLeaveType(entity);
			return result;
		}
		public async Task<bool> DeleteLeaveType(int id)
		{
			return await _leavetype.DeleteLeaveType(id);
		}
	}
}
