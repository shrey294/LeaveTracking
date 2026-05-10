using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface ILeaveType
	{
		Task<List<LeaveType>> leaveTypes();
		Task<bool> AddLeaveType(LeaveType leave);
		Task<bool> EditLeaveType(LeaveType leave);
		Task<bool> DeleteLeaveType(int id);
	}
}
