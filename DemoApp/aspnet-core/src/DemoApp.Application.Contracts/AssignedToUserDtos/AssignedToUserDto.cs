using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.AssignedToUserDtos
{
    public class AssignedToUserDto : EntityDto<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ToDoId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
