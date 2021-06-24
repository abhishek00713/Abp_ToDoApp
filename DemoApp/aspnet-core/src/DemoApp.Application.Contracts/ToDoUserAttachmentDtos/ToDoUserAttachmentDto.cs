using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoUserAttachmentDtos
{
    public class ToDoUserAttachmentDto : EntityDto<Guid>
    {
        public Guid ToDOUserTaskId { get; set; }

        public string AttachmentCaption { get; set; }
        public string AttachmentFile { get; set; }
    }
}
