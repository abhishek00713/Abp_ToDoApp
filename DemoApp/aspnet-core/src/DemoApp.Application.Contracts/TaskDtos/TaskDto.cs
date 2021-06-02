using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.TaskDtos
{
    public class TaskDto : EntityDto<Guid>
    {
        public string TaskName { get; set; }
        
    }
}
