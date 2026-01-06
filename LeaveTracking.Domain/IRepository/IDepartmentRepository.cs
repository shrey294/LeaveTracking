using LeaveTracking.Domain.DTO;
using LeaveTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.IRepository
{
	public interface IDepartmentRepository
	{
		Task<List<DepartmentDTO>> GetDepartmentList();
		Task<DepartmentDTO> Get_Department_ByID(int id);
		Task<bool> Insert_Department(DepartmentMaster department);
		Task<bool> Update_Department(DepartmentMaster departmentMaster);
		Task<bool> Delete_Department(int id);
	}
}
