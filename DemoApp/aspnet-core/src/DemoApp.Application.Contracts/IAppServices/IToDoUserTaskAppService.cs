using DemoApp.ToDoUserTaskDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IToDoUserTaskAppService : IApplicationService
    {
        Task<ToDoUserTaskDto> GetAsync(Guid id);
        Task<PagedResultDto<ToDoUserTaskDto>> GetListAsync(GetToDoUserTaskListDto input);
        Task<ToDoUserTaskDto> CreateASync(CreateToDoUserTaskDto input);
        Task UpdateAsync(Guid id, UpdateToDoUserTaskDto input);
        Task DeleteAsync(Guid id);
    }
}
