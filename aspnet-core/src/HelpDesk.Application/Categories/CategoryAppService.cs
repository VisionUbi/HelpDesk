using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using HelpDesk.Categorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Categories
{
    public class CategoryAppService : HelpDeskAppServiceBase
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryAppService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public virtual async Task<CategoryDto> GetCategoryForEdit(EntityDto<long> input)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync((int)input.Id);

            var output = new CategoryDto
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name
            };

            return output;
        }
        public virtual async Task CreateOrEdit(CategoryDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await CreateCategory(input);
            }
            else
            {
                await UpdateCategory(input);
            }
        }
     
        public async Task UpdateCategory(CategoryDto input)
        {
            var existingcategory = await _categoryRepository.GetAll().Where(a => a.Id == input.Id).FirstOrDefaultAsync();

            existingcategory.Name = input.Name;
            existingcategory.Description = input.Description;
            existingcategory.LastModifierUserId = AbpSession.UserId;

            await _categoryRepository.UpdateAsync(existingcategory);
        }
        public async Task CreateCategory(CategoryDto input)
        {
            var existingCategory = await _categoryRepository.GetAll().Where(a => a.Name == input.Name).FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                throw new UserFriendlyException("This name already exist");
            }
            var data = new Category
            {
                Name = input.Name,
                Description = input.Description,
                CreatorUserId = AbpSession.UserId
            };
            await _categoryRepository.InsertAsync(data);
        }
        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(id);
            }
        }
        public async Task<List<CategoryDto>> GetAllCategorys(string description)
        {
            var Category = await _categoryRepository.GetAll().Where(a => a.Description == description).ToListAsync();
            var CategoryNameList = Category.Select(Category => new CategoryDto
            {
                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description
            }).ToList();
            return CategoryNameList;
        }
    
}
}
