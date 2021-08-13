using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.AssignedToUserDtos
{
    public class CreateAssignedToUserDto
    {
        [Required]
        public String UserId { get; set; }
        [Required]
        public String ToDoId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
