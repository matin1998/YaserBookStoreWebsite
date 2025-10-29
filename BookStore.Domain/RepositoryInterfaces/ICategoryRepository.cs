using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface ICategoryRepository
{
    List<string> GetListOfCategories();

    Task AddCategoryToDataBase(Category category);

    Task<Category> GetACategoryByIdAsync(int categoryId);

    Task EditACategory(Category category);

    Task DeleteACategory(Category category);
}
