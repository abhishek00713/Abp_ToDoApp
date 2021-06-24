using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoUserTaskDtos
{
    public class ToDoUserTaskDto : EntityDto<Guid>
    {
        public Guid ToDoId { get; set; }
        public Guid StatusId { get; set; }
    }
}
