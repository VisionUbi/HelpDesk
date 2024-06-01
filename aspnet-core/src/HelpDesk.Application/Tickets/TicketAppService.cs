using Abp.Domain.Repositories;
using Abp.UI;
using HelpDesk.Categorys;
using HelpDesk.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    public class TicketAppService : HelpDeskAppServiceBase
    {
        private readonly IRepository<Ticket> _ticketRepository;
        public TicketAppService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task CreateCategory(TicketDto input)
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
                CategoryId = input.CategoryId
            };
            await _ticketRepository.InsertAsync(data);
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
