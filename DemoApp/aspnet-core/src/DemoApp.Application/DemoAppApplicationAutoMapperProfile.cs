using AutoMapper;
using DemoApp.AppEntities;
using DemoApp.CategoryDTOs;
using DemoApp.DTOs;
using DemoApp.StatusDtos;

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





        }
    }
}
