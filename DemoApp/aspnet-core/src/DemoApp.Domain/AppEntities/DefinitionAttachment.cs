using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DemoApp.AppEntities
{
    public class DefinitionAttachment : FullAuditedAggregateRoot<Guid>
    {
        public Guid ToDoId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFileURL { get; set; }

        //Navigation Property
        public virtual ToDo ToDos { get; set; }
    }
}
