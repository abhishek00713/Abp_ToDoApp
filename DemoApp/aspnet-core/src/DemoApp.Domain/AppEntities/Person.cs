using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Person : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }


    }
}
