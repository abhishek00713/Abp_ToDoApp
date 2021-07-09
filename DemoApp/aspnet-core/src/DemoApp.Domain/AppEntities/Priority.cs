using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Priority : FullAuditedAggregateRoot<Guid>
    {
        public string PriorityName { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }

        public Priority()
        {
            ToDos = new HashSet<ToDo>();
        }
    }
}
