using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.ToDo_AssignedDtos;
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
    public class ToDoAssignedAppService : DemoAppAppService, ItoDoAssignedAppService
    {
        private readonly IRepository<ToDoAssignedTo, Guid> _todoAssignedtoRepository;

        public ToDoAssignedAppService(IRepository<ToDoAssignedTo, Guid> todoAssignedtoRepository)
        {
            _todoAssignedtoRepository = todoAssignedtoRepository;
        }

        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _todoAssignedtoRepository.DeleteAsync(id);
        }

        public async Task<ToDoAssignedDto> GetAsync(Guid id)
        {
            ToDoAssignedTo toDoAssignedTo = await _todoAssignedtoRepository.GetAsync(id);

            return ObjectMapper.Map<ToDoAssignedTo, ToDoAssignedDto>(toDoAssignedTo);
        }

        public async Task<PagedResultDto<ToDoAssignedDto>> GetListAsync(GetToDo_Assigned input)
        {

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDoAssignedTo.ToDoId);
            }

            List<ToDoAssignedTo> ToDoAssigned = await _todoAssignedtoRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting

                );



            var totalcount = await AsyncExecuter.CountAsync(
                _todoAssignedtoRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    ToDoAssignedTo => ToDoAssignedTo.AssignedTo.Equals(input.Filter)
                    )
                );



            List<ToDoAssignedDto> todoassigneddtos =
                ObjectMapper.Map<List<ToDoAssignedTo>, List<ToDoAssignedDto>>(ToDoAssigned);

            PagedResultDto<ToDoAssignedDto> result = new PagedResultDto<ToDoAssignedDto>(
                    totalcount, todoassigneddtos
                );



            return result;
        }

        public async Task UpdateAsync(Guid id, UpdateToDo_Assigned input)
        {
            var todoassigned = await _todoAssignedtoRepository.GetAsync(id);

            todoassigned.AssignedTo = input.AssignedTo;
            todoassigned.ToDoId = input.ToDoId;





            await _todoAssignedtoRepository.UpdateAsync(todoassigned);
        }
    }
}
