using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.StatusDtos
{
    public class UpdateStatusDto
    {

        [Required, MaxLength(100)]
        public string StatusName { get; set; }

    }
}
