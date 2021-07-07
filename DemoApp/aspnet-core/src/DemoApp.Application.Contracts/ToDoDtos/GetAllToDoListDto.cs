using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDoDtos
{
    public class GetAllToDoListDto : PagedAndSortedResultRequestDto
    {
        public string filterCategory { get; set; }
        public string filterPriority { get; set; }
        public string filterStatus { get; set; }
        public string filterTask { get; set; }
    }
}
