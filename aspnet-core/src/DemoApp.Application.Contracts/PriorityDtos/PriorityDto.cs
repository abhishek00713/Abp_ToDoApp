using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.PriorityDtos
{
    public class PriorityDto: EntityDto<Guid>
    {
        public string PriorityName { get; set; }
        public bool Discontinued { get; set; }
    }
}
