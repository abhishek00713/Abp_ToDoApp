using DemoApp.AppEntities;
using DemoApp.IAppServices;
using DemoApp.Permissions;
using DemoApp.ToDoDtos;
using DemoApp.UserDtos;
using DemoApp.Users;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace DemoApp.AppServices
{
    //[Authorize(DemoAppPermissions.DemoApp.Default_Define_ToDo)]
    public class ToDoAppService : DemoAppAppService, IToDoAppService
    {
        private readonly IRepository<ToDo, Guid> _todoRepository;

        private readonly IRepository<AppUser, Guid> _userRepository;

        private readonly IRepository<Volo.Abp.Identity.IdentityRole, Guid> _roleRepository;

        private readonly IdentityUserManager UserManager;

        





        public ToDoAppService(IRepository<AppUser, Guid> userRepository, IRepository<ToDo, Guid> todoRepository
                                , IRepository<Volo.Abp.Identity.IdentityRole, Guid> roleRepository
                                , IdentityUserManager userManager
            )
        {
            _userRepository = userRepository;
            _todoRepository = todoRepository;
            _roleRepository = roleRepository;
            UserManager = userManager;

            
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

        public string GetCurrentUserRole()
        {
            var role = CurrentUser.Roles[0];
            return role;
        }

        public async Task<PagedResultDto<ToDoDto>> GetListAsync(GetToDoListDto input)
        {
             if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDo.Date);
            }


            var todoList = _todoRepository

            .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.Remarks.Contains(input.Filter)
          ).OrderBy(p => p.Date)
          .Skip(input.SkipCount)
            .Take(input.MaxResultCount)

            .ToList(); ;

            //List<ToDoTask> tasks = await _taskRepository.GetPagedListAsync(


            //    input.SkipCount,
            //    input.MaxResultCount,
            //    input.Sorting
            //    );

            var totalcount = await AsyncExecuter.CountAsync(
                _todoRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    todo => todo.Remarks.Contains(input.Filter)
                    )
                );



            List<ToDoDto> todoDtos =
                ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(todoList);

            PagedResultDto<ToDoDto> result = new PagedResultDto<ToDoDto>(
                    totalcount, todoDtos
                );



            return result;
        }

        public async Task<IList<IdentityUserDto>> GetUserList(GetToDoListDto input)
        {
            var user = CurrentUser;
            

            var usersOfRole = await UserManager.GetUsersInRoleAsync("user");

            IList<IdentityUserDto> retu = new List<IdentityUserDto>();

            foreach(IdentityUser i in usersOfRole)
            {
                retu.Add(ObjectMapper.Map<IdentityUser, IdentityUserDto>(i));

            }

            

           

            return retu;

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
