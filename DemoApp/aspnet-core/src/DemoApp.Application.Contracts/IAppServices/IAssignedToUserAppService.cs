using DemoApp.AssignedToUserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IAssignedToUserAppService : IApplicationService
    {
        Task<AssignedToUserDto> GetAsync(Guid id);
        Task<PagedResultDto<AssignedToUserDto>> GetListAsync(GetAssignedToUserListDto input);
        Task<AssignedToUserDto> CreateASync(CreateAssignedToUserDto input);
        Task UpdateAsync(Guid id, UpdateAssignedToUserDto input);
        Task DeleteAsync(Guid id);
    }
}
