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
        public Guid StatusId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid TaskId { get; set; }

      


        //public ICollection<CategoryDto> Categories { get; set; }
        //public ICollection<StatusDto> Statuses { get; set; }
        //public ICollection<PriorityDto> Priorities { get; set; }

        //public TaskDto todotask { get; set; }

        public DateTime Date { get; set; }

        public Guid AssignedBy { get; set; }

        public string Remarks { get; set; }
    }
}
