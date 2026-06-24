using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task AddCategoryToDataBase(Category category)
    {
        var categories = _categoryRepository.GetListOfCategories();
        bool flag = false;
        foreach (var c in categories)
        {
            if (c.CategoryTitle == category.CategoryTitle)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            await _categoryRepository.EditACategory(category);
        }
        else
        {
            await _categoryRepository.AddCategoryToDataBase(category);
        }
    }

    public async Task DeleteACategory(Category category)
    {
        await _categoryRepository.DeleteACategory(category);
    }

    public async Task EditACategory(Category category)
    {
        await _categoryRepository.EditACategory(category);
    }

    public async Task<Category> GetACategoryByIdAsync(int categoryId)
    {
        Category category = await _categoryRepository.GetACategoryByIdAsync(categoryId);
        return category;
    }

    public  List<Category> GetListOFCategories()
    {
        return _categoryRepository.GetListOfCategories();
    }

   
}
