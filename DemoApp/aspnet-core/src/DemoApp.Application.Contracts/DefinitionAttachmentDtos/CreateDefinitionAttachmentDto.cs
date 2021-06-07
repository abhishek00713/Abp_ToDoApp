using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class CreateDefinitionAttachmentDto
    {
        public Guid ToDoId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFileURL { get; set; }
    }
}
