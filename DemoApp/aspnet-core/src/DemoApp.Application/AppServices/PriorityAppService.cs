using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.PriorityDtos;
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
    public class PriorityAppService : DemoAppAppService, IPriorityAppService    {

        private readonly IRepository<Priority, Guid> _priorityRepository;
        public PriorityAppService(IRepository<Priority, Guid> priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }
        [Authorize]
        public async Task<PriorityDto> CreateASync(CreatePriorityDto input)
        {

            Priority priorities =
              ObjectMapper.Map<CreatePriorityDto, Priority>(input);



            var priority = await _priorityRepository.InsertAsync(priorities);


            return ObjectMapper.Map<Priority, PriorityDto>(priority);
        }
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _priorityRepository.DeleteAsync(id);
        }
        [Authorize]
        public async Task<PriorityDto> GetAsync(Guid id)
        {
            Priority priority = await _priorityRepository.GetAsync(id);

            return ObjectMapper.Map<Priority, PriorityDto>(priority);
        }

        public async Task<PagedResultDto<PriorityDto>> GetFullList(GetPriorityListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Priority.PriorityName);
            }

            List<Priority> priorities = await _priorityRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _priorityRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Priority => Priority.PriorityName.Contains(input.Filter)
                    )
                );



            List<PriorityDto> priorityDtos =
                ObjectMapper.Map<List<Priority>, List<PriorityDto>>(priorities);

            PagedResultDto<PriorityDto> result = new PagedResultDto<PriorityDto>(
                    totalcount, priorityDtos
                );



            return result;
        }

        [Authorize]
        public async Task<PagedResultDto<PriorityDto>> GetListAsync(GetPriorityListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Priority.PriorityName);
            }


            var priorityList = _priorityRepository

            .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.PriorityName.Contains(input.Filter)
          ).OrderBy(p => p.PriorityName)
          .Skip(input.SkipCount)
            .Take(input.MaxResultCount)

            .ToList(); ;


            var totalcount = await AsyncExecuter.CountAsync(
                _priorityRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    priority => priority.PriorityName.Contains(input.Filter)
                    )
                );



            List<PriorityDto> priorityDtos =
                ObjectMapper.Map<List<Priority>, List<PriorityDto>>(priorityList);

            PagedResultDto<PriorityDto> result = new PagedResultDto<PriorityDto>(
                    totalcount, priorityDtos
                );



            return result;
        }
        [Authorize]
        public async Task UpdateAsync(Guid id, UpdatePriorityDto input)
        {
            var priority = await _priorityRepository.GetAsync(id);

            priority.PriorityName = input.PriorityName;
            



            await _priorityRepository.UpdateAsync(priority);
        }
    }
}
