using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.PriorityDtos
{
    public class GetPriorityListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
