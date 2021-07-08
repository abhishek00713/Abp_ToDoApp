using DemoApp.ToDo_AssignedDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface ItoDoAssignedAppService : IApplicationService
    {
        Task<ToDoAssignedDto> GetAsync(Guid id);
        Task<PagedResultDto<ToDoAssignedDto>> GetListAsync(GetToDo_Assigned input);

        Task<ToDoAssignedDto> CreateASync(CreateToDo_Assigned input);
        Task UpdateAsync(Guid id, UpdateToDo_Assigned input);
        Task DeleteAsync(Guid id);
    }
}
