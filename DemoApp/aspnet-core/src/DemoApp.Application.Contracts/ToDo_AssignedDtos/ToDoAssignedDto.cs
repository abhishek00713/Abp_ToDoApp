using System;
using System.Collections.Generic;

using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.ToDo_AssignedDtos
{
    public class ToDoAssignedDto : EntityDto<Guid>
    {

        public Guid ToDoId { get; set; }


        
        public Guid AssignedTo { get; set; }

        


    }
}
