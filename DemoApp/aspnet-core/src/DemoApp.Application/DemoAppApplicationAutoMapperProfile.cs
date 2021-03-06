using AutoMapper;
using DemoApp.AppEntities;
using DemoApp.AssignedToUserDtos;
using DemoApp.CategoryDTOs;
using DemoApp.DefinitionAttachmentDtos;
using DemoApp.DTOs;
using DemoApp.PriorityDtos;
using DemoApp.StatusDtos;
using DemoApp.TaskDtos;
using DemoApp.ToDo_AssignedDtos;
using DemoApp.ToDoDtos;
using DemoApp.ToDoUserAttachmentDtos;
using DemoApp.ToDoUserTaskDtos;
using DemoApp.UserDtos;
using DemoApp.Users;
using Volo.Abp.Identity;

namespace DemoApp
{
    public class DemoAppApplicationAutoMapperProfile : Profile
    {
        public DemoAppApplicationAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, GetProductListDto>();
            CreateMap<UpdateProductDto, Product>();


            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryListDto>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Status, StatusDto>();
            CreateMap<CreateStatusDto, Status>();
            CreateMap<Status, GetStatusListDto>();
            CreateMap<UpdateStatusDto, Status>();

            CreateMap<Priority, PriorityDto>();
            CreateMap<CreatePriorityDto, Priority>();
            CreateMap<Priority, GetPriorityListDto>();
            CreateMap<UpdatePriorityDto, Priority>();

            


            CreateMap<ToDoTask, TaskDto>();
            CreateMap<CreateTaskDto, ToDoTask>();
            CreateMap<ToDoTask, GetTaskListDto>();
            CreateMap<UpdateTaskDto, ToDoTask>();


            CreateMap<ToDo, ToDoDto>();
            CreateMap<CreateToDoDto, ToDo>();
            CreateMap<ToDo, GetToDoListDto>();
            CreateMap<ToDo, GetAllToDoListDto>();
            CreateMap<UpdateToDoDto, ToDo>();

            CreateMap<DefinitionAttachment, DefinitionAttachmentDto>();
            CreateMap<CreateDefinitionAttachmentDto, DefinitionAttachment>();
            CreateMap<DefinitionAttachment, GetDefinitionAttachmentListDto>();
            CreateMap<UpdateDefinitionAttachmentDto, DefinitionAttachment>();

            CreateMap<AssignedToUser, AssignedToUserDto>();
            CreateMap<CreateAssignedToUserDto, AssignedToUser>();
            CreateMap<AssignedToUser, GetAssignedToUserListDto>();
            CreateMap<UpdateAssignedToUserDto, AssignedToUser>();



            CreateMap<ToDoUserAttachment, ToDoUserAttachmentDto>();
            CreateMap<CreateToDoUserAttachmentDto, ToDoUserAttachment>();
            CreateMap<ToDoUserAttachment, GetToDoUserAttachmentListDto>();
            CreateMap<UpdateToDoUserAtatchmentDto, ToDoUserAttachment>();


            CreateMap<ToDoUserTask, ToDoUserTaskDto>();
            CreateMap<CreateToDoUserTaskDto, ToDoUserTask>();
            CreateMap<ToDoUserTask, GetToDoUserTaskListDto>();
            CreateMap<UpdateToDoUserTaskDto, ToDoUserTask>();

            CreateMap<AppUser, AbpUserDto>();

        }
    }
}
