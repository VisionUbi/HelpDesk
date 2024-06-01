using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Departments
{
    [Table("Departments")]
    public class Department : FullAuditedEntity
    {
        public virtual string Name { get; set; }
    }
}
