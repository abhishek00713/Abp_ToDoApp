using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.TaskDtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

//Task
namespace DemoApp.AppServices
{
    public class TaskAppService : DemoAppAppService, ITaskAppService
    {
        private readonly IRepository<ToDoTask, Guid> _taskRepository;
        public TaskAppService(IRepository<ToDoTask, Guid> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [Authorize]
        public async Task<TaskDto> CreateASync(CreateTaskDto input)
        {
            ToDoTask tasks =
            ObjectMapper.Map<CreateTaskDto, ToDoTask>(input);



            var task = await _taskRepository.InsertAsync(tasks);


            return ObjectMapper.Map<ToDoTask, TaskDto>(task);
        }
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }
        [Authorize]
        public async Task<TaskDto> GetAsync(Guid id)
        {
            ToDoTask task = await _taskRepository.GetAsync(id);

            return ObjectMapper.Map<ToDoTask, TaskDto>(task);

        }

        [Authorize]
        public async Task<PagedResultDto<TaskDto>> GetFullList(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDoTask.TaskName);
            }

            List<ToDoTask> tasks = await _taskRepository.GetListAsync();

            var totalcount = await AsyncExecuter.CountAsync(
                _taskRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Task => Task.TaskName.Contains(input.Filter)
                    )
                );



            List<TaskDto> taskDtos =
                ObjectMapper.Map<List<ToDoTask>, List<TaskDto>>(tasks);

            PagedResultDto<TaskDto> result = new PagedResultDto<TaskDto>(
                    totalcount, taskDtos
                );



            return result;

        }

        [Authorize]
        public async Task<PagedResultDto<TaskDto>> GetListAsync(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDoTask.TaskName);
            }


            var taskList = _taskRepository
           
            .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.TaskName.Contains(input.Filter) 
          ).OrderBy(p => p.TaskName)
          .Skip(input.SkipCount)
            .Take(input.MaxResultCount)

            .ToList(); ;

            //List<ToDoTask> tasks = await _taskRepository.GetPagedListAsync(


            //    input.SkipCount,
            //    input.MaxResultCount,
            //    input.Sorting
            //    );

            var totalcount = await AsyncExecuter.CountAsync(
                _taskRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Task1 => Task1.TaskName.Contains(input.Filter)
                    )
                );



            List<TaskDto> taskDtos =
                ObjectMapper.Map<List<ToDoTask>, List<TaskDto>>(taskList);

            PagedResultDto<TaskDto> result = new PagedResultDto<TaskDto>(
                    totalcount, taskDtos
                );



            return result;
        }
        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateTaskDto input)
        {
            var task = await _taskRepository.GetAsync(id);

            task.TaskName = input.TaskName;




            await _taskRepository.UpdateAsync(task);
        }
    }
}