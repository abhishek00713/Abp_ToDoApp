using AutoMapper;
using DemoApp.AppEntities;
using DemoApp.CategoryDTOs;
using DemoApp.DTOs;
using DemoApp.PriorityDtos;
using DemoApp.StatusDtos;
using DemoApp.TaskDtos;
using DemoApp.ToDo_AssignedDtos;
using DemoApp.ToDoDtos;

namespace DemoApp
{
    public class DemoAppApplicationAutoMapperProfile : Profile
    {
        public DemoAppApplicationAutoMapperProfile()
        {
            
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
            CreateMap<UpdateToDoDto, ToDo>();

            CreateMap<ToDoAssignedTo, ToDoAssignedDto>();
            CreateMap<CreateToDo_Assigned, ToDoAssignedTo>();
            CreateMap<ToDoAssignedTo, GetToDo_Assigned>();
            CreateMap<UpdateToDo_Assigned, ToDoAssignedTo>();


        }
    }
}
