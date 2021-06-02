using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        

        [Required, MaxLength(100)]
        public string CategoryName { get; set; }

       
    }
}
