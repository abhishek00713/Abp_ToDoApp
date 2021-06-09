using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.AssignedToUserDtos
{
    public class GetAssignedToUserListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
