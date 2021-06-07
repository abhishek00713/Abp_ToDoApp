using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.ToDo_AssignedDtos
{
    public class CreateToDo_Assigned
    {
        [Required]
        public Guid ToDoId { get; set; }

        [Required]
        public Guid AssignedTo { get; set; }

        [Required]
        public Guid AssignedBy { get; set; }
    }
}
