using DemoApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class AssignedToUser : FullAuditedAggregateRoot<Guid>
    {
        public Guid ToDoId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        //navigation property
        public virtual ToDo ToDos { get; set; }
        public virtual AppUser AbpUser { get; set; }

    }
}

