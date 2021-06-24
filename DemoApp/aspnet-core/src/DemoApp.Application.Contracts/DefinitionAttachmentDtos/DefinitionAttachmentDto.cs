using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class DefinitionAttachmentDto : EntityDto<Guid>
    {
        public Guid TodoId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFileURL { get; set; }
    }
}
