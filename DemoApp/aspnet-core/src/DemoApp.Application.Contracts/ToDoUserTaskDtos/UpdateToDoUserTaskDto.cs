using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.ToDoUserTaskDtos
{
    public class UpdateToDoUserTaskDto
    {

        [Required]

        public Guid ToDoId { get; set; }

        [Required]
        public Guid StatusId { get; set; }
    }
}
