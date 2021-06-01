using DemoApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);
        Task<ProductDto> CreateASync(CreateProductDto input);
        Task UpdateAsync(Guid id, UpdateProductDto input);
        Task DeleteAsync(Guid id);
    }
}
