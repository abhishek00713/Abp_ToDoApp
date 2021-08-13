using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDo_AssignedDtos
{
    public class GetToDo_Assigned : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
