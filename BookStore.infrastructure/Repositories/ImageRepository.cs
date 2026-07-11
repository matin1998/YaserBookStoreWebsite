using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly BookStoreDbContext _context;
    #region Ctor
    public ImageRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    #endregion
    public async Task AddImageToDataBase(Image image)
    {
        await _context.Images.AddAsync(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnImage(Image image)
    {
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
    }

    public async Task EditAnImage(Image image)
    {
        _context.Images.Update(image);
        await _context.SaveChangesAsync();
    }

    public Task<Image> GetAnImageByIdAsync(int imageId)
    {
        return _context.Images.FirstOrDefaultAsync(p => p.Id == imageId);
    }

    public List<Image> GetListOfImages()
    {
        return _context.Images.ToList();
    }
    public async Task<List<Image>> GetImagesByBookIdAsync(int bookId)
    {
        return await _context.Images
            .Where(x => x.BookId == bookId)
            .ToListAsync();
    }
}
