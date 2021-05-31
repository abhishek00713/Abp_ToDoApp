using DemoApp.AppEntities;
using DemoApp.IAppServices;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Services;
using DemoApp.DTOs;
using Volo.Abp.Application.Dtos;

namespace DemoApp.AppServices
{
    [Authorize]
    public class ProductAppService : DemoAppAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        public async Task<ProductDto> CreateASync(CreateProductDto input)
        {
            Product products =
               ObjectMapper.Map<CreateProductDto, Product>(input);

            var product = await _productRepository.InsertAsync(products);



            //return new ProductDto();
            //for test commit
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Name);
            }

            List<Product> products = await _productRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
                );

            var totalcount = await AsyncExecuter.CountAsync(
                _productRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    product => product.Name.Contains(input.Filter)
                    )
                );



            //return new PagedResultDto<ProductDto>(
            //    totalcount,
            //    products.Select(t => new ProductDto()
            //    { Id = t.Id, Name = t.Name, Price = t.Price, Quantity = t.Quantity, ProductType = t.ProductType })
            //    .ToList()
            //    );


            //Map entities to DTOs
            List<ProductDto> productDtos =
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products);

            PagedResultDto<ProductDto> result = new PagedResultDto<ProductDto>(
                    totalcount, productDtos
                );



            return result;

        }

        [Authorize]
        public async Task UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);

            Product products =
               ObjectMapper.Map<UpdateProductDto, Product>(input);



            await _productRepository.UpdateAsync(product);
        }
    }

    //[Authorize]
    //public class ProductAppService : CrudAppService<Product, ProductDto, Guid,
    //    PagedAndSortedResultRequestDto, CreateProductDto>
    //{
    //    public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
    //    {
    //    }
    //}
}
