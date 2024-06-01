using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    public class TicketDto
    {
        public  string Subject { get; set; }
        public  string Description { get; set; }
        public  string Phone { get; set; }
        public  int CategoryId { get; set; }
        public  int DepartmentId { get; set; }
        public  int AssignedTo { get; set; }
        public  int Priority { get; set; }
        public  string Remarks { get; set; }
    }
}
