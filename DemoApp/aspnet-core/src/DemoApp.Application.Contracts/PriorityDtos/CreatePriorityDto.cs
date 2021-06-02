using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.PriorityDtos
{
    public class CreatePriorityDto
    {

        [Required, MaxLength(100)]
        public string PriorityName { get; set; }

     
    }
}
