using DemoApp.StatusDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IStatusAppService : IApplicationService
    {
        Task<StatusDto> GetAsync(Guid id);
        Task<PagedResultDto<StatusDto>> GetListAsync(GetStatusListDto input);

        Task<PagedResultDto<StatusDto>> GetFullList(GetStatusListDto input);
        Task<StatusDto> CreateASync(CreateStatusDto input);
        Task UpdateAsync(Guid id, UpdateStatusDto input);
        Task DeleteAsync(Guid id);
    }
}
