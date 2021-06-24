using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class ToDoUserAttachment : FullAuditedAggregateRoot<Guid>
    {
        public Guid ToDOUserTaskId { get; set; }

        public string AttachmentCaption { get; set; }
        public string AttachmentFile { get; set; }

    }
}
