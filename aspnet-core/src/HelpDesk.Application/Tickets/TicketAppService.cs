using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.UI;
using HelpDesk.Authorization.Users;
using HelpDesk.Categorys;
using HelpDesk.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    public class TicketAppService : HelpDeskAppServiceBase
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<User, long> _usersRepository;
        public TicketAppService(IRepository<Ticket> ticketRepository, IRepository<UserRole, long> userRoleRepository = null, IRepository<Department> departmentRepository = null, IRepository<User, long> usersRepository = null)
        {
            _ticketRepository = ticketRepository;
            _userRoleRepository = userRoleRepository;
            _departmentRepository = departmentRepository;
            _usersRepository = usersRepository;
        }
        public async Task CreateCategory(TicketDto input)
        {
            if (!string.IsNullOrEmpty(input.Id) && int.TryParse(input.Id, out int id))
            {
                var exisitingTicket = await _ticketRepository.FirstOrDefaultAsync(int.Parse(input.Id));
                if(exisitingTicket != null)
                {
                    exisitingTicket.Status = input.Status;
                    exisitingTicket.Remarks = input.Remarks;
                    exisitingTicket.AssignedTo = input.AssignedTo;
                }

            }
            else
            {
                var data = new Ticket
                {
                    Subject = input.Subject,
                    Description = input.Description,
                    DepartmentId = input.DepartmentId,
                    Phone = input.Phone,
                    Priority = input.Priority,
                    Remarks = input.Remarks,
                    AssignedTo = input.AssignedTo,
                    CategoryId = input.CategoryId,
                    DepartmentType = input.DepartmentType,
                };
                await _ticketRepository.InsertAsync(data);
            }
           
        }
      
        public async Task<List<TicketDto>> GetTickets()
        {
            
            var results = new List<Ticket>();
            var userId = (long)AbpSession.UserId;
            var role = await GetUserRoleByUserId(userId);
            var departments = await _departmentRepository.GetAllAsync();
            var users = await _usersRepository.GetAllAsync();
            if (role != null && (role.RoleId == 1 || role.RoleId == 2))
            {
                 results = await _ticketRepository.GetAllListAsync();
            }
            else if(role != null && role.RoleId == 3)
            {
                results = await _ticketRepository.GetAll().Where(a => a.DepartmentType == "IT").ToListAsync();
            }
            else if (role != null && role.RoleId == 4)
            {
                results = await _ticketRepository.GetAll().Where(a => a.DepartmentType == "Maintainance").ToListAsync();
            }
            else
            {
                results = await _ticketRepository.GetAll().Where(a => a.CreatorUserId == userId).ToListAsync();
            }
            var finalResult = new List<TicketDto>();
            foreach(var ticket in results)
            {
                var assignedToUser = users.FirstOrDefault(a => a.Id == ticket.AssignedTo);
                var ticketDto = new TicketDto
                {
                    Id = ticket.Id.ToString(),
                    Subject = ticket.Subject,
                    Description = ticket.Description,
                    Email = ticket.Email,
                    Phone = ticket.Phone,
                    CategoryId = ticket.CategoryId,
                    DepartmentId = ticket.DepartmentId,
                    AssignedTo = ticket.AssignedTo,
                    AssignedToName = assignedToUser != null ? new UserListDto
                    {
                        Name = assignedToUser.Name,
                        Value = assignedToUser.Id
                    } : null,
                    Priority = ticket.Priority,
                    Remarks = ticket.Remarks,
                    Status = ticket.Status,
                    DepartmentType = ticket.DepartmentType,
                    CreationDate = ticket.CreationTime.ToString(),
                    CreatedBy = users.FirstOrDefault(a => a.Id == ticket.CreatorUserId)?.Name,
                    DepartmentName = departments.FirstOrDefault(a=> a.Id == ticket.DepartmentId)?.Name,
                    Users = users.Select(a => new UserListDto
                    {
                        Name = a.Name,
                        Value = a.Id
                    }).ToList()
                };

                finalResult.Add(ticketDto);
            }
            finalResult = finalResult.OrderByDescending(t => DateTime.Parse(t.CreationDate)).ToList();

            return finalResult;

        }
        public async Task<UserRole> GetUserRoleByUserId(long userId)
        {

            var userRole = await _userRoleRepository.GetAll().FirstOrDefaultAsync(u => u.UserId == userId);
            if (userRole != null)
            {
                return userRole;
            }
            else
            {
                return null;
            }

        }
        //public async Task<List<CategoryDto>> GetAllCategorys()
        //{
        //    var Category = await _ticketRepository.GetAllAsync();
        //    var CategoryNameList = Category.Select(Category => new CategoryDto
        //    {
        //        Id = Category.Id,
        //        Name = Category.Name
        //    }).ToList();
        //    return CategoryNameList;
        //}
    }
}
