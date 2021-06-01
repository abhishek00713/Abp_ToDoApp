using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Priority : FullAuditedAggregateRoot<Guid>
    {
        public string PriorityName { get; set; }

        public bool Discontinued { get; set; }
    }
}
