using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.Permissions;
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
    [Authorize(DemoAppPermissions.DemoApp.Default_Define_ToDo)]
    public class ToDoAppService : DemoAppAppService, IToDoAppService
    {
        private readonly IRepository<ToDo, Guid> _todoRepository;
        public ToDoAppService(IRepository<ToDo, Guid> todoRepository)
        {
            _todoRepository = todoRepository;
        }
        [Authorize(DemoAppPermissions.DemoApp.Create_Define_ToDo)]
        public async Task<ToDoDto> CreateASync(CreateToDoDto input)
        {
            ToDo todos =
            ObjectMapper.Map<CreateToDoDto, ToDo>(input);



            var todo = await _todoRepository.InsertAsync(todos);


            return ObjectMapper.Map<ToDo, ToDoDto>(todo);
        }

        [Authorize(DemoAppPermissions.DemoApp.Delete_Define_ToDo)]
        public async Task DeleteAsync(Guid id)
        {
            await _todoRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<ToDoDto>> GetAllListAsync(GetAllToDoListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDo.Id);
            }

            var queryable = await _todoRepository.WithDetailsAsync(
                (t => t.Category),
                (t => t.Priority),
                (t => t.Status),
                (t => t.Tasks)
                );


            //filter category
            if (!(input.filterCategory.IsNullOrWhiteSpace()))
            {
                queryable = queryable.Where(t => input.filterCategory.Contains(t.Category.CategoryName));
            }
            if (!(input.filterPriority.IsNullOrWhiteSpace()))
            {
                queryable = queryable.Where(t => input.filterPriority.Contains(t.Priority.PriorityName));
            }
            if (!(input.filterStatus.IsNullOrWhiteSpace()))
            {
                queryable = queryable.Where(t => input.filterStatus.Contains(t.Status.StatusName));
            }
            if (!(input.filterTask.IsNullOrWhiteSpace()))
            {
                queryable = queryable.Where(t => input.filterTask.Contains(t.Tasks.TaskName));
            }

            var totalcount = await AsyncExecuter.CountAsync(
                queryable);

            List<ToDoDto> toDos =
                ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(queryable.ToList());

            PagedResultDto<ToDoDto> result = new PagedResultDto<ToDoDto>(
                    totalcount, toDos
                );



            return result;
        }

        public async Task<ToDoDto> GetAsync(Guid id)
        {
            ToDo toDo = await _todoRepository.GetAsync(id);
            return ObjectMapper.Map<ToDo, ToDoDto>(toDo);
        }

        public async Task<PagedResultDto<ToDoDto>> GetListAsync(GetToDoListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDoDto.Date);
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

        [Authorize(DemoAppPermissions.DemoApp.Update_Define_ToDo)]
        public async Task UpdateAsync(Guid id, UpdateToDoDto input)
        {
            var toDos = await _todoRepository.GetAsync(id);
            toDos.CategoryId = input.CategoryId;
            toDos.StatusId = input.StatusId;
            toDos.PriorityId = input.PriorityId;
            toDos.TaskId = input.TaskId;
            toDos.Date = input.Date;
            await _todoRepository.UpdateAsync(toDos);
        }
    }
}
