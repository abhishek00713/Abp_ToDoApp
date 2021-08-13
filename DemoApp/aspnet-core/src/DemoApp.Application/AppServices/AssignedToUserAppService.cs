using DemoApp.AppEntities;
using DemoApp.AssignedToUserDtos;
using DemoApp.IAppServices;
using DemoApp.UserDtos;
using DemoApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace DemoApp.AppServices
{
    public class AssignedToUserAppService : DemoAppAppService, IAssignedToUserAppService
    {
        private readonly IRepository<AssignedToUser, Guid> _assignedtouserRepository;

        private readonly IRepository<AppUser, Guid> _userRepository;

        protected IIdentityRoleRepository RoleRepository { get; }
        protected IIdentityLinkUserRepository linkUserRepository { get; }
     
        private readonly IdentityUserManager UserManager;
        protected IIdentityUserRepository UserRepository { get; }

        public AssignedToUserAppService(IRepository<AssignedToUser, Guid> assignedtouserRepository
                                        , IRepository<AppUser, Guid> userRepository
                                        , IdentityUserManager userManager,
            IIdentityUserRepository usersRepository,
            IIdentityRoleRepository roleRepository,
            IIdentityUserAppService userAppService,
            IIdentityLinkUserRepository link,
            IIdentityUserLookupAppService lookup
            )
        {

            UserManager = userManager;
            UserRepository = usersRepository;
            RoleRepository = roleRepository;
            _assignedtouserRepository = assignedtouserRepository;
            _userRepository = userRepository;

        }

        public async Task<AssignedToUserDto> CreateASync(CreateAssignedToUserDto input)
        {
            Guid todoid = new Guid(input.ToDoId);
            Guid userid = new Guid(input.UserId);

            AssignedToUser temp = new AssignedToUser();

            temp.UserId = userid;
            temp.ToDoId = todoid;
            temp.IsActive = input.IsActive;
            

            //AssignedToUser assignedusers =
            //  ObjectMapper.Map<CreateAssignedToUserDto, AssignedToUser>(input);


            var assigneduser = await _assignedtouserRepository.InsertAsync(temp);


            return ObjectMapper.Map<AssignedToUser, AssignedToUserDto>(assigneduser);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _assignedtouserRepository.DeleteAsync(id);
        }

        public async Task<AssignedToUserDto> GetAsync(Guid id)
        {
            AssignedToUser assignedUser = await _assignedtouserRepository.GetAsync(id);
            return ObjectMapper.Map<AssignedToUser, AssignedToUserDto>(assignedUser);
        }

        public async Task<PagedResultDto<AssignedToUserDto>> GetListAsync(GetAssignedToUserListDto input)
        {
            

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(AssignedToUserDto.IsActive);
            }

            List<AssignedToUser> assignedUsers = await _assignedtouserRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _assignedtouserRepository
                );

            List<AssignedToUserDto> assignedToUsers =
                ObjectMapper.Map<List<AssignedToUser>, List<AssignedToUserDto>>(assignedUsers);

            PagedResultDto<AssignedToUserDto> result = new PagedResultDto<AssignedToUserDto>(
                    totalcount, assignedToUsers
                );

            

            return result;
        }

        public String Get_Todo_id(Guid todoid, Guid userid)
        {
            var assigned_id = _assignedtouserRepository
                .WhereIf(
                !todoid.Equals(null),
                p => p.ToDoId.ToString().Equals(todoid.ToString())
                )
                .WhereIf(
                !userid.Equals(null),
                p => p.UserId.ToString().Equals(userid.ToString())
                ).FirstOrDefault();

            

            return assigned_id.Id.ToString();

        }

        public PagedResultDto<AbpUserDto> Get_Todo_userList(Guid todoid)
        {
            List<AppUser> userlist = new List<AppUser>();

             List<AssignedToUser> todo_user_list = _assignedtouserRepository
            .WhereIf(
                !todoid.Equals(null),
                p => p.ToDoId.Equals(todoid)
          ).OrderBy(p => p.CreationTime)
        

            .ToList(); 
                 
                 
            
            for(var i =0; i<todo_user_list.Count; i++)
            {
                var user = _userRepository.Where(x => x.Id == todo_user_list[i].UserId).FirstOrDefault();
                userlist.Add(user);

            }
            var totalcount = userlist.Count();
            List<AbpUserDto> userDtos =
              ObjectMapper.Map<List<AppUser>, List<AbpUserDto>>(userlist);

            PagedResultDto<AbpUserDto> result = new PagedResultDto<AbpUserDto>(
                  totalcount, userDtos
              );

            return result;
        }

        public  List<Guid> Get_User_assigned_todo_listAsync(Guid userid)
        {
            var todo_list = _assignedtouserRepository.WhereIf(
                !userid.Equals(null),
                p => p.UserId.ToString().Equals(userid.ToString())
                ).OrderBy(p => p.CreationTime)
                .Select(p=>p.ToDoId)
                .ToList();

            return todo_list;
        }

        public async Task UpdateAsync(Guid id, UpdateAssignedToUserDto input)
        {
            var assignedToUsers = await _assignedtouserRepository.GetAsync(id);
            assignedToUsers.IsActive = input.IsActive;
            await _assignedtouserRepository.UpdateAsync(assignedToUsers);
        }
    }
}
