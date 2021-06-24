using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.ToDoDtos;
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
    public class ToDoAppService : DemoAppAppService, IToDoAppService
    {
        private readonly IRepository<ToDo, Guid> _todoRepository;
        public ToDoAppService(IRepository<ToDo, Guid> todoRepository)
        {
            _todoRepository = todoRepository;
        }
        [Authorize]
        public async Task<ToDoDto> CreateASync(CreateToDoDto input)
        {
            ToDo todos =
            ObjectMapper.Map<CreateToDoDto, ToDo>(input);



            var todo = await _todoRepository.InsertAsync(todos);


            return ObjectMapper.Map<ToDo, ToDoDto>(todo);
        }

        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _todoRepository.DeleteAsync(id);
        }

        [Authorize]
        public async Task<ToDoDto> GetAsync(Guid id)
        {
            ToDo toDo = await _todoRepository.GetAsync(id);

            return ObjectMapper.Map<ToDo, ToDoDto>(toDo);
        }

        [Authorize]
        public async Task<PagedResultDto<ToDoDto>> GetListAsync(GetToDoListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Task1.TaskName);
            }

            List<ToDo> todos = await _todoRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _todoRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    ToDo => ToDo.Remarks.Contains(input.Filter)
                    )
                );



            List<ToDoDto> toDos =
                ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(todos);

            PagedResultDto<ToDoDto> result = new PagedResultDto<ToDoDto>(
                    totalcount, toDos
                );



            return result;
        }

        [Authorize]
        public Task UpdateAsync(Guid id, UpdateToDoDto input)
        {
            throw new NotImplementedException();
        }
    }
}
