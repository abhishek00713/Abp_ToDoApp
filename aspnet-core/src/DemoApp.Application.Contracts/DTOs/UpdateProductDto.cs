using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.DTOs
{
    public class UpdateProductDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
    }
}
