using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.ToDoUserTaskDtos;
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
    public class ToDoUserTaskAppService : DemoAppAppService, IToDoUserTaskAppService
    {
        private readonly IRepository<ToDoUserTask, Guid> _todoUserTaskRepository;
        public ToDoUserTaskAppService(IRepository<ToDoUserTask, Guid> todoUserTaskRepository)
        {
            _todoUserTaskRepository = todoUserTaskRepository;
        }
        [Authorize]
        public async Task<ToDoUserTaskDto> CreateASync(CreateToDoUserTaskDto input)
        {
                        ToDoUserTask tasks =
            ObjectMapper.Map<CreateToDoUserTaskDto, ToDoUserTask>(input);



            var task = await _todoUserTaskRepository.InsertAsync(tasks);


            return ObjectMapper.Map<ToDoUserTask, ToDoUserTaskDto>(task);
        }
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _todoUserTaskRepository.DeleteAsync(id);
        }
        [Authorize]
        public async Task<ToDoUserTaskDto> GetAsync(Guid id)
        {
            ToDoUserTask task = await _todoUserTaskRepository.GetAsync(id);

            return ObjectMapper.Map<ToDoUserTask, ToDoUserTaskDto>(task);
        }
        [Authorize]
        public Task<PagedResultDto<ToDoUserTaskDto>> GetListAsync(GetToDoUserTaskListDto input)
        {
            throw new NotImplementedException();
        }
        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateToDoUserTaskDto input)
        {
            var task = await _todoUserTaskRepository.GetAsync(id);

            task.ToDoId = input.ToDoId;
            task.StatusId = input.StatusId;
            await _todoUserTaskRepository.UpdateAsync(task);
        }
    }
}
