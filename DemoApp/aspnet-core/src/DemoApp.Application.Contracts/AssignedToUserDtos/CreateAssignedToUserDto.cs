using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.AssignedToUserDtos
{
    public class CreateAssignedToUserDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ToDoId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
