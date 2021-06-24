using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoUserTaskDtos
{
    public class GetToDoUserTaskListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
