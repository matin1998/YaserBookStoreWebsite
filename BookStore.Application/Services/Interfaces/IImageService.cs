using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface IImageService
{
    Task AddImageAsync(IFormFile imageFile, int bookId);

    Task DeleteImageAsync(int imageId);

    Task EditImageAsync(int imageId, IFormFile newImage);

    Task DeleteImagesByBookIdAsync(int bookId);

    Task<Image> GetImageByIdAsync(int imageId);

    Task<List<Image>> GetImagesByBookIdAsync(int bookId);

    List<Image> GetAllImages();

    Task SetMainImageAsync(int imageId);
}
