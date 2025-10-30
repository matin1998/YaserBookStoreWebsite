using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface ICategoryService
{
    Task AddCategoryToDataBase(Category category);
    List<string> GetListOFCategories();

    Task<Category> GetACategoryByIdAsync(int categoryId);

    Task EditACategory(Category category);

    Task DeleteACategory(Category category);
}
