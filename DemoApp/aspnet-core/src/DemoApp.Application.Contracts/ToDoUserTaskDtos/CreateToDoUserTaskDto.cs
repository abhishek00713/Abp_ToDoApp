using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.ToDoUserTaskDtos
{
    public class CreateToDoUserTaskDto
    {
        [Required]
        public Guid ToDoId { get; set; }

        [Required]
        public Guid StatusId { get; set; }
    }
}
