using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.TaskDtos
{
    public class GetTaskListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
