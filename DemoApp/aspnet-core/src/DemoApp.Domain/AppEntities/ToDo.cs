using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class ToDo : FullAuditedAggregateRoot<Guid>
    {
      


        //public ICollection<Category> Categories { get; set; }
        //public ICollection<Status> Statuses { get; set; }
        //public ICollection<Priority> Priorities { get; set; }

        //public Task1 todotask { get; set; }


        public Guid CategoryId { get; set; }
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid TaskId { get; set; }


       

        public DateTime Date { get; set; }

        public String Time { get; set; }

        

        public Guid AssignedBy { get; set; }

        public string Remarks { get; set; }


    }
}
