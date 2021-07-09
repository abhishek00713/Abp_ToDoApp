using DemoApp.CategoryDTOs;
using DemoApp.PriorityDtos;
using DemoApp.StatusDtos;
using DemoApp.TaskDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoDtos
{
    public class ToDoDto : EntityDto<Guid>
    {

        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public Guid StatusId { get; set; }
        public StatusDto Status { get; set; }
        public Guid PriorityId { get; set; }
        public PriorityDto Priority { get; set; }
        public Guid TaskId { get; set; }
        public TaskDto Task { get; set; }

        public DateTime Date { get; set; }
        public Guid AssignedBy { get; set; }
        public string Remarks { get; set; }
    }
}
