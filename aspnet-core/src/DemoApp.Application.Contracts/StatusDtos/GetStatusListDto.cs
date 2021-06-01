using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.StatusDtos
{
    public class GetStatusListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
