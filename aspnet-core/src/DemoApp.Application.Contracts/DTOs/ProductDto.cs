using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.DTOs
{
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
    }
}
