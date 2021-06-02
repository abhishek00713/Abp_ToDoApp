using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.CategoryDTOs
{
    public class CategoryDto : EntityDto<Guid>
    {
       

        public string CategoryName { get; set; }

        
    }
}
