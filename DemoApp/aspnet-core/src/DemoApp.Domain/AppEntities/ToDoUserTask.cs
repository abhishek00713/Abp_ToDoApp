using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class ToDoUserTask : FullAuditedAggregateRoot<Guid>
    {
        public Guid ToDoId { get; set; }
        public Guid StatusId { get; set; }
    }
}
