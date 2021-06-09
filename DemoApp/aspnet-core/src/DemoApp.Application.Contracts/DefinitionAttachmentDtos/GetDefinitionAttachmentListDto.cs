using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class GetDefinitionAttachmentListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
