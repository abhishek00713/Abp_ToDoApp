using AutoMapper;
using DemoApp.AppEntities;
using DemoApp.CategoryDTOs;
using DemoApp.DTOs;
using DemoApp.PriorityDtos;
using DemoApp.StatusDtos;
using DemoApp.TaskDtos;
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

            CreateMap<Task1, TaskDto>();
            CreateMap<CreateTaskDto, Task1>();
            CreateMap<Task1, GetTaskListDto>();
            CreateMap<UpdateTaskDto, Task1>();


            CreateMap<ToDo, ToDoDto>();
            CreateMap<CreateToDoDto, ToDo>();
            CreateMap<ToDo, GetToDoListDto>();
            CreateMap<UpdateToDoDto, ToDo>();


        }
    }
}
