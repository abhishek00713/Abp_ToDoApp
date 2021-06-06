using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoDtos
{
    public class GetToDoListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
