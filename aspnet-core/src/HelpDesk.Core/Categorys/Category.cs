using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Categorys
{
    [Table("Categorys")]
    public class Category : FullAuditedEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
