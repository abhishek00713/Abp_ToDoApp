using DemoApp.AssignedToUserDtos;
using DemoApp.UserDtos;
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
        PagedResultDto<AbpUserDto> Get_Todo_userList(Guid todoid);

        String Get_Todo_id(Guid todoid, Guid userid);
        Task UpdateAsync(Guid id, UpdateAssignedToUserDto input);
        Task DeleteAsync(Guid id);

        List<Guid> Get_User_assigned_todo_listAsync(Guid userid);
    }
}
