using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        

        public string CategoryName { get; set; }

        //public ToDo ToDo { get; set; }


    }   
}
