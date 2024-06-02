using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using HelpDesk.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Departments
{
    public class DepartmentAppService : HelpDeskAppServiceBase
    {
        private readonly IRepository<Department> _departmentRepository;
        public DepartmentAppService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public virtual async Task<DepartmentDto> GetdepartmentForEdit(EntityDto<long> input)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync((int)input.Id);

            var output = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return output;
        }
        public virtual async Task CreateOrEdit(DepartmentDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await CreateDepartment(input.Name);
            }
            else
            {
                await UpdateDepartment(input);
            }
        }
        public async Task CreateDepartment(string input)
        {
            var existingDepartment = await _departmentRepository.GetAll().Where(a => a.Name == input).FirstOrDefaultAsync();
            if (existingDepartment != null)
            {
                throw new UserFriendlyException("This name already exist");
            }
            var data = new Department
            {
                Name = input,
                CreatorUserId = AbpSession.UserId
            };
            await _departmentRepository.InsertAsync(data);
        }
        public async Task UpdateDepartment(DepartmentDto input)
        {
            var existingDepartment = await _departmentRepository.GetAll().Where(a => a.Id == input.Id).FirstOrDefaultAsync();

            existingDepartment.Name = input.Name;
            existingDepartment.LastModifierUserId = AbpSession.UserId;

            await _departmentRepository.UpdateAsync(existingDepartment);
        }
        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            var department = await _departmentRepository.GetAllAsync();
            var departmentNameList = department.Select(department => new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            }).ToList();
            return departmentNameList;
        }
        public async Task DeleteDepartment(int id)
        {
            var department = await _departmentRepository.FirstOrDefaultAsync(id);
            if (department != null)
            {
                await _departmentRepository.DeleteAsync(id);
            }
        }
    }
}
