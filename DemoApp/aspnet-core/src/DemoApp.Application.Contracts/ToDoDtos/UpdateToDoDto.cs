using DemoApp.CategoryDTOs;
using DemoApp.PriorityDtos;
using DemoApp.StatusDtos;
using DemoApp.TaskDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.ToDoDtos
{
    public class UpdateToDoDto
    {

        [Required]

        public Guid CategoryId { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [Required]
        public Guid PriorityId { get; set; }

        [Required]
        public Guid TaskId { get; set; }


        //[Required]
        //  public ICollection<CategoryDto> Categories { get; set; }

        // [Required]
        // public ICollection<StatusDto> Statuses { get; set; }

        // [Required]
        // public ICollection<PriorityDto> Priorities { get; set; }

        // [Required]
        // public TaskDto todotask { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid AssignedBy { get; set; }

        public string Remarks { get; set; }
    }
}
