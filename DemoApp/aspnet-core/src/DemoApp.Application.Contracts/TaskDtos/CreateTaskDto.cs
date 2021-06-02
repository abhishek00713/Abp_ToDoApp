using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.TaskDtos
{
    public class CreateTaskDto
    {

        [Required, MaxLength(100)]
        public string TaskName { get; set; }

     
    }
}
