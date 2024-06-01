using Abp.Domain.Repositories;
using Abp.UI;
using HelpDesk.Categorys;
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
        public async Task CreateCategory(string input)
        {
            var existingCategory = await _categoryRepository.GetAll().Where(a => a.Name == input).FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                throw new UserFriendlyException("This name already exist");
            }
            var data = new Category
            {
                Name = input,
                CreatorUserId = AbpSession.UserId
            };
            await _categoryRepository.InsertAsync(data);
        }
        public async Task<List<CategoryDto>> GetAllCategorys()
        {
            var Category = await _categoryRepository.GetAllAsync();
            var CategoryNameList = Category.Select(Category => new CategoryDto
            {
                Id = Category.Id,
                Name = Category.Name
            }).ToList();
            return CategoryNameList;
        }
    
}
}
