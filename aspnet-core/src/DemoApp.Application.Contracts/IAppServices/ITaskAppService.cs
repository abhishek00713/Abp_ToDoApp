using DemoApp.TaskDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface ITaskAppService : IApplicationService
    {
        Task<TaskDto> GetAsync(Guid id);
        Task<PagedResultDto<TaskDto>> GetListAsync(GetTaskListDto input);
        Task<TaskDto> CreateASync(CreateTaskDto input);
        Task UpdateAsync(Guid id, UpdateTaskDto input);
        Task DeleteAsync(Guid id);
    }
}
