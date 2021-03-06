using DemoApp.AppEntities;
using DemoApp.CategoryDTOs;
using DemoApp.IAppServices;
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
    public class CategoryAppService : DemoAppAppService, ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryAppService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        public async Task<CategoryDto> CreateASync(CreateCategoryDto input)
        {
            Category categories =
              ObjectMapper.Map<CreateCategoryDto, Category>(input);

           

            var category = await _categoryRepository.InsertAsync(categories);


            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            //softdelete isdelete true

            await _categoryRepository.DeleteAsync(id);
        }

        [Authorize]
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            Category category = await _categoryRepository.GetAsync(id);

            return ObjectMapper.Map<Category, CategoryDto>(category);

            
        }


        [Authorize]
        public async Task<PagedResultDto<CategoryDto>> GetFullList(GetCategoryListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Category.CategoryName);
            }

            List<Category> categories = await _categoryRepository.GetListAsync();

            var totalcount = await AsyncExecuter.CountAsync(
                _categoryRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    Category => Category.CategoryName.Contains(input.Filter)
                    )
                );



            List<CategoryDto> categoryDtos =
                ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);

            PagedResultDto<CategoryDto> result = new PagedResultDto<CategoryDto>(
                    totalcount, categoryDtos
                );



            return result;

        }

        [Authorize]

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Category.CategoryName);
            }

            

            var categoryList = _categoryRepository

          .WhereIf(
              !input.Filter.IsNullOrEmpty(),
              p => p.CategoryName.Contains(input.Filter)
        ).OrderBy(p => p.CategoryName)
        .Skip(input.SkipCount)
          .Take(input.MaxResultCount)

          .ToList(); ;

            var totalcount = await AsyncExecuter.CountAsync(
           _categoryRepository.WhereIf(
               !input.Filter.IsNullOrWhiteSpace(),
               category => category.CategoryName.Contains(input.Filter)
               )
           );


            List<CategoryDto> categoryDos =
               ObjectMapper.Map<List<Category>, List<CategoryDto>>(categoryList);

            PagedResultDto<CategoryDto> result = new PagedResultDto<CategoryDto>(
                    totalcount, categoryDos
                );



            return result;



        }

        [Authorize]

        public async Task UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            var category = await _categoryRepository.GetAsync(id);

            category.CategoryName = input.CategoryName;                        
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
