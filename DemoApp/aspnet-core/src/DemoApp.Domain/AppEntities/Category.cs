using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string CategoryName { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }
        public Category()
        {
            ToDos = new HashSet<ToDo>();
        }
    }
}
