using Abp.Domain.Entities.Auditing;
using HelpDesk.Categorys;
using HelpDesk.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    [Table("Tickets")]
    public class Ticket : FullAuditedEntity
    {
        public virtual string Subject { get; set; }
        public virtual string Description { get; set; }
        public virtual string Email { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual int Phone { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual int AssignedTo { get; set; }
        public virtual int Priority { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string DepartmentType { get; set; }

        [ForeignKey("DepartmentId")]
        public Department DepartmentFK { get; set; }

        [ForeignKey("CategoryId")]
        public Category CategoryFk { get; set; }
    }
}
