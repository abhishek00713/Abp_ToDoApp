using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.ToDoDtos;
using DemoApp.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace DemoApp.AppServices
{
    public class ToDoAppService : DemoAppAppService, IToDoAppService
    {

        private readonly ICurrentUser _currentUser;

        private readonly IRepository<ToDo, Guid> _todoRepository;


        public ToDoAppService(IRepository<ToDo, Guid> todoRepository, ICurrentUser currentUser
                              
                    )
        {
            _todoRepository = todoRepository;

            _currentUser = currentUser;


        }
        [Authorize]
        public async Task<ToDoDto> CreateASync(CreateToDoDto input)
        {
            
            

            ToDo todos =
            ObjectMapper.Map<CreateToDoDto, ToDo>(input);

            todos.Date = input.Date.Date;
            todos.Time = input.Date.ToShortTimeString();

            var todo = await _todoRepository.InsertAsync(todos);

            todo.AssignedBy = (Guid)_currentUser.Id;





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
                input.Sorting = nameof(ToDo.CategoryId);
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
        public async Task UpdateAsync(Guid id, UpdateToDoDto input)
        {

            var toDos = await _todoRepository.GetAsync(id);
            toDos.CategoryId = input.CategoryId;
            toDos.StatusId = input.StatusId;
            toDos.PriorityId = input.PriorityId;
            toDos.TaskId = input.TaskId;
            toDos.Date = input.Date;
            toDos.Remarks = input.Remarks;
            await _todoRepository.UpdateAsync(toDos);
        }
    }
}
