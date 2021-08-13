using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class ToDoTask : FullAuditedAggregateRoot<Guid>
    {
        public string TaskName { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }

        public ToDoTask()
        {
            ToDos = new HashSet<ToDo>();
        }
    }
}
