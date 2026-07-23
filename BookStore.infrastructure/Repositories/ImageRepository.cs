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

public class ImageRepository : BaseRepository<Image>,IImageRepository
{
    #region Ctor
    public ImageRepository(BookStoreDbContext context)
        :base(context)
    {
    }

    #endregion
    public async Task AddImageToDataBase(Image image)
    {
        await AddAsync(image);
    }

    public async Task DeleteAnImage(Image image)
    {
         await DeleteAsync(image);
    }

    public async Task EditAnImage(Image image)
    {
        await UpdateAsync(image);
    }

    public async Task<Image> GetAnImageByIdAsync(int imageId)
    {
        return await _context.Images.FirstOrDefaultAsync(p => p.Id == imageId);
    }

    public async Task<List<Image>> GetListOfImages()
    {
        return await GetAllAsync();
    }
    public async Task<List<Image>> GetImagesByBookIdAsync(long bookId)
    {
        return await _context.Images
            .Where(x => x.BookId == bookId)
            .ToListAsync();
    }
}
