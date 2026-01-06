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
	public class DepartmentService
	{
		private readonly IDepartmentRepository _department;
		public DepartmentService(IDepartmentRepository department)
		{
			_department = department;
		}
		public async Task<List<DepartmentDTO>> GetDepartmentList()
		{
			return await _department.GetDepartmentList();
		}
		public async Task<DepartmentDTO> Get_Department_ByID(int id)
		{
			return await _department.Get_Department_ByID(id);
		}
		public async Task<bool> Insert_Department(DepartmentDTO department)
		{
			var domainentity = new DepartmentMaster
			{
				DepartmentName = department.DepartmentName,
				DepartmentCode = department.DepartmentCode,
				Location = department.location
			};
			var result =await _department.Insert_Department(domainentity);
			return result;
		}
		public async Task<bool> Update_Department(DepartmentDTO department)
		{
			var domainentity = new DepartmentMaster
			{
				DeptId = department.Id??0,
				DepartmentName = department.DepartmentName,
				DepartmentCode = department.DepartmentCode,
				Location = department.location
			};
			var result = await _department.Update_Department(domainentity);
			return result;
		}
		public async Task<bool> Delete_Department(int id)
		{
			return await _department.Delete_Department(id);
		}
	}
}
