using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.Permissions;
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
    [Authorize(DemoAppPermissions.DemoApp.Default_Define_ToDo)]
    public class TaskAppService : DemoAppAppService, ITaskAppService
    {
        private readonly IRepository<Task1, Guid> _taskRepository;
        public TaskAppService(IRepository<Task1, Guid> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [Authorize(DemoAppPermissions.DemoApp.Create_Define_ToDo)]
        public async Task<TaskDto> CreateASync(CreateTaskDto input)
        {
            Task1 tasks =
            ObjectMapper.Map<CreateTaskDto, Task1>(input);



            var task = await _taskRepository.InsertAsync(tasks);


            return ObjectMapper.Map<Task1, TaskDto>(task);
        }
        [Authorize(DemoAppPermissions.DemoApp.Delete_Define_ToDo)]
        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<TaskDto> GetAsync(Guid id)
        {
            Task1 task = await _taskRepository.GetAsync(id);

            return ObjectMapper.Map<Task1, TaskDto>(task);

        }

        public async Task<PagedResultDto<TaskDto>> GetListAsync(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Task1.TaskName);
            }

            List<Task1> tasks = await _taskRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _taskRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Task1 => Task1.TaskName.Contains(input.Filter)
                    )
                );



            List<TaskDto> taskDtos =
                ObjectMapper.Map<List<Task1>, List<TaskDto>>(tasks);

            PagedResultDto<TaskDto> result = new PagedResultDto<TaskDto>(
                    totalcount, taskDtos
                );



            return result;
        }
        [Authorize(DemoAppPermissions.DemoApp.Update_Define_ToDo)]
        public async Task UpdateAsync(Guid id, UpdateTaskDto input)
        {
            var task = await _taskRepository.GetAsync(id);

            task.TaskName = input.TaskName;




            await _taskRepository.UpdateAsync(task);
        }
    }
}
