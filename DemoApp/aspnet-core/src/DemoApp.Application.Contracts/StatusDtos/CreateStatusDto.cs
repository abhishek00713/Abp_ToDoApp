using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.StatusDtos
{
    public class CreateStatusDto
    {

        [Required, MaxLength(100)]
        public string StatusName { get; set; }

       
    }
}
