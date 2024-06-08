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
                Description = department.Description,
                Name = department.Name
            };

            return output;
        }
        public virtual async Task CreateOrEdit(DepartmentDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await CreateDepartment(input);
            }
            else
            {
                await UpdateDepartment(input);
            }
        }
        public async Task CreateDepartment(DepartmentDto input)
        {
            var existingDepartment = await _departmentRepository.GetAll().Where(a => a.Name == input.Name && a.Description == input.Description).FirstOrDefaultAsync();
            if (existingDepartment != null)
            {
                throw new UserFriendlyException("This name already exist");
            }
            var data = new Department
            {
                Name = input.Name,
                Description = input.Description,
                CreatorUserId = AbpSession.UserId
            };
            await _departmentRepository.InsertAsync(data);
        }
        public async Task UpdateDepartment(DepartmentDto input)
        {
            var existingDepartment = await _departmentRepository.GetAll().Where(a => a.Id == input.Id).FirstOrDefaultAsync();

            existingDepartment.Name = input.Name;
            existingDepartment.Description = input.Description;
            existingDepartment.LastModifierUserId = AbpSession.UserId;

            await _departmentRepository.UpdateAsync(existingDepartment);
        }
        public async Task<List<DepartmentDto>> GetAllDepartments(string description)
        { 
            var department = await _departmentRepository.GetAll().Where(a => a.Description == description).ToListAsync();
            var departmentNameList = department.Select(department => new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
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
