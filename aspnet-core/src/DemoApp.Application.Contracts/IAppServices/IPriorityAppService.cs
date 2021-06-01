using DemoApp.PriorityDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IPriorityAppService : IApplicationService
    {
        Task<PriorityDto> GetAsync(Guid id);
        Task<PagedResultDto<PriorityDto>> GetListAsync(GetPriorityListDto input);
        Task<PriorityDto> CreateASync(CreatePriorityDto input);
        Task UpdateAsync(Guid id, UpdatePriorityDto input);
        Task DeleteAsync(Guid id);
    }
}
