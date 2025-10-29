using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDbContext _context;
 #region Ctor
        public CategoryRepository(BookStoreDbContext context)
        {
            _context = context;
        }

#endregion
        public async Task AddCategoryToDataBase(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteACategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditACategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public Task<Category> GetACategoryByIdAsync(int categoryId)
        {
            return _context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);
        }

        public List<string> GetListOfCategories()
        {
            return _context.Categories
                .Select(b => b.CategoryTitle)
                .ToList();
        }
    }
}
