using DemoApp.AppEntities;
using DemoApp.AssignedToUserDtos;
using DemoApp.IAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace DemoApp.AppServices
{
    public class AssignedToUserAppService : DemoAppAppService, IAssignedToUserAppService
    {
        private readonly IRepository<AssignedToUser, Guid> _assignedtouserRepository;

        public AssignedToUserAppService(IRepository<AssignedToUser, Guid> assignedtouserRepository)
        {
            _assignedtouserRepository = assignedtouserRepository;
        }

        public async Task<AssignedToUserDto> CreateASync(CreateAssignedToUserDto input)
        {
            AssignedToUser assignedusers =
              ObjectMapper.Map<CreateAssignedToUserDto, AssignedToUser>(input);


            var assigneduser = await _assignedtouserRepository.InsertAsync(assignedusers);


            return ObjectMapper.Map<AssignedToUser, AssignedToUserDto>(assigneduser);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _assignedtouserRepository.DeleteAsync(id);
        }

        public async Task<AssignedToUserDto> GetAsync(Guid id)
        {
            AssignedToUser assignedUser = await _assignedtouserRepository.GetAsync(id);
            return ObjectMapper.Map<AssignedToUser, AssignedToUserDto>(assignedUser);
        }

        public async Task<PagedResultDto<AssignedToUserDto>> GetListAsync(GetAssignedToUserListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(AssignedToUserDto.IsActive);
            }

            List<AssignedToUser> assignedUsers = await _assignedtouserRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _assignedtouserRepository
                );

            List<AssignedToUserDto> assignedToUsers =
                ObjectMapper.Map<List<AssignedToUser>, List<AssignedToUserDto>>(assignedUsers);

            PagedResultDto<AssignedToUserDto> result = new PagedResultDto<AssignedToUserDto>(
                    totalcount, assignedToUsers
                );



            return result;
        }

        public async Task UpdateAsync(Guid id, UpdateAssignedToUserDto input)
        {
            var assignedToUsers = await _assignedtouserRepository.GetAsync(id);
            assignedToUsers.IsActive = input.IsActive;
            await _assignedtouserRepository.UpdateAsync(assignedToUsers);
        }
    }
}
