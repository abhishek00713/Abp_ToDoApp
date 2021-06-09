using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.AssignedToUserDtos
{
    public class UpdateAssignedToUserDto
    {
        [Required]
        public bool IsActive { get; set; }


    }
}
