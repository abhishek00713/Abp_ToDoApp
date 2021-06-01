using DemoApp.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<CategoryDto> GetAsync(Guid id);
        Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input);
        Task<CategoryDto> CreateASync(CreateCategoryDto input);
        Task UpdateAsync(Guid id, UpdateCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}
