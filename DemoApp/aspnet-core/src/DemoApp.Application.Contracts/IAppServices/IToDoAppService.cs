using DemoApp.ToDoDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IToDoAppService : IApplicationService
    {
        Task<ToDoDto> GetAsync(Guid id);
        Task<PagedResultDto<ToDoDto>> GetListAsync(GetToDoListDto input);
        Task<ToDoDto> CreateASync(CreateToDoDto input);
        Task UpdateAsync(Guid id, UpdateToDoDto input);
        Task DeleteAsync(Guid id);
    }
}
