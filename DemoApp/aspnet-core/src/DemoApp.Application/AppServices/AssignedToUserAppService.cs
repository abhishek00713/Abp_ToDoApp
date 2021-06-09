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

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedToUserDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<AssignedToUserDto>> GetListAsync(GetAssignedToUserListDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateAssignedToUserDto input)
        {
            throw new NotImplementedException();
        }
    }
}
