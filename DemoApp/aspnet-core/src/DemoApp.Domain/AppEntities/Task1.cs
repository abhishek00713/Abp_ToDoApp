using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Task1 : FullAuditedAggregateRoot<Guid>
    {
        public string TaskName { get; set; }
       
    }
}
