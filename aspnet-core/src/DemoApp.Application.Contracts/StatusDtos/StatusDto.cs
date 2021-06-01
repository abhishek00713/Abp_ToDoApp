using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.StatusDtos
{
    public class StatusDto : EntityDto<Guid>
    {
        public string StatusName { get; set; }

        public bool Discontinued { get; set; }
    }
}
