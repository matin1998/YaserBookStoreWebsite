using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public ImageService(
        IImageRepository imageRepository,
        IFileService fileService,
        IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task AddImageAsync(
        IFormFile imageFile,
        long bookId)
    {
        string imageName =
            await _fileService.SaveImageAsync(imageFile);

        Image image = new Image
        {
            ImageName = imageName,
            BookId = bookId,
            IsMainImage = false
        };

        await _imageRepository.AddImageToDataBase(image);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteImageAsync(int imageId)
    {
        var image =
            await _imageRepository.GetAnImageByIdAsync(imageId);

        if (image == null)
            return;

        _fileService.DeleteImage(image.ImageName);

        await _imageRepository.DeleteAnImage(image);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EditImageAsync(
        int imageId,
        IFormFile newImage)
    {
        var image =
            await _imageRepository.GetAnImageByIdAsync(imageId);

        if (image == null)
            return;

        _fileService.DeleteImage(image.ImageName);

        string newImageName =
            await _fileService.SaveImageAsync(newImage);

        image.ImageName = newImageName;

        await _imageRepository.EditAnImage(image);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Image> GetImageByIdAsync(int imageId)
    {
        return await _imageRepository
            .GetAnImageByIdAsync(imageId);
    }

    public async Task<List<Image>> GetAllImages()
    {
        return await _imageRepository.GetListOfImages();
    }

    public async Task DeleteImagesByBookIdAsync(long bookId)
    {
        var images = await _imageRepository.GetImagesByBookIdAsync(bookId);

        foreach (var image in images)
        {
            _fileService.DeleteImage(image.ImageName);

            await _imageRepository.DeleteAnImage(image);
        }
        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task<List<Image>> GetImagesByBookIdAsync(long bookId)
    {
        var images = await _imageRepository.GetImagesByBookIdAsync(bookId);
        return images;
    }

    public async Task SetMainImageAsync(int imageId)
    {
        var image =
            await _imageRepository.GetAnImageByIdAsync(imageId);

        if (image == null)
            return;

        var images =
            (await _imageRepository.GetListOfImages())
            .Where(x => x.BookId == image.BookId)
            .ToList();

        foreach (var item in images)
        {
            item.IsMainImage = false;

            /*await _imageRepository.EditAnImage(item);*/
        }

        image.IsMainImage = true;

        await _imageRepository.EditAnImage(image);
        await _unitOfWork.SaveChangesAsync();
    }
}
