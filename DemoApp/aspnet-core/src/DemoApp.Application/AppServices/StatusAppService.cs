using DemoApp.AppEntities;

using DemoApp.IAppServices;
using DemoApp.Permissions;
using DemoApp.StatusDtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace DemoApp.AppServices
{
    [Authorize(DemoAppPermissions.DemoApp.Default_Define_ToDo)]
    public class StatusAppService : DemoAppAppService, IStatusAppService
    {
        private readonly IRepository<Status, Guid> _statusRepository;
        private readonly Status _statusManager;

        public StatusAppService(IRepository<Status, Guid> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<StatusDto> GetAsync(Guid id)
        {
            Status status = await _statusRepository.GetAsync(id);

            return ObjectMapper.Map<Status, StatusDto>(status);

        }

        public async Task<PagedResultDto<StatusDto>> GetListAsync(GetStatusListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Status.StatusName);
            }

            List<Status> statuses = await _statusRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _statusRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Status => Status.StatusName.Contains(input.Filter)

                    )
                );



            List<StatusDto> statusDtos =
                ObjectMapper.Map<List<Status>, List<StatusDto>>(statuses);

            PagedResultDto<StatusDto> result = new PagedResultDto<StatusDto>(
                    totalcount, statusDtos
                );



            return result;
        }


        [Authorize(DemoAppPermissions.DemoApp.Create_Define_ToDo)]
        public async Task<StatusDto> CreateASync(CreateStatusDto input)
        {
            Status status = ObjectMapper.Map<CreateStatusDto, Status>(input);

            var st = await _statusRepository.InsertAsync(status);




            return ObjectMapper.Map<Status, StatusDto>(st);
        }


        [Authorize(DemoAppPermissions.DemoApp.Update_Define_ToDo)]
        public async Task UpdateAsync(Guid id, UpdateStatusDto input)
        {
            var status = await _statusRepository.GetAsync(id);

            status.StatusName = input.StatusName;


            await _statusRepository.UpdateAsync(status);


        }


        [Authorize(DemoAppPermissions.DemoApp.Delete_Define_ToDo)]
        public async Task DeleteAsync(Guid id)
        {
            await _statusRepository.DeleteAsync(id);
        }
    }
}
