using HelpDesk.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    public class TicketDto
    {
        public string Id { get; set; }
        public  string Subject { get; set; }
        public  string Email { get; set; }
        public  string Description { get; set; }
        public  string Phone { get; set; }
        public  int CategoryId { get; set; }
        public  int DepartmentId { get; set; }
        public  int AssignedTo { get; set; }
        public  int Priority { get; set; }
        public  string Remarks { get; set; }
        public  string CreationDate { get; set; }
        public  string CreatedBy { get; set; }
        public  string DepartmentName { get; set; }
        public UserListDto AssignedToName { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual string DepartmentType { get; set; }
        public virtual List<UserListDto> Users { get; set; }

    }
    public class UserListDto
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }

}

