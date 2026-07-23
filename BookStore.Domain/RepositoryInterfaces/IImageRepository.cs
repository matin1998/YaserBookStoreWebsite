using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces
{
    public interface IImageRepository:IBaseRepository<Image>
    {
        Task<List<Image>> GetListOfImages();

        Task AddImageToDataBase(Image image);

        Task<Image> GetAnImageByIdAsync(int imageId);

        Task EditAnImage(Image image);

        Task DeleteAnImage(Image image);
        Task<List<Image>> GetImagesByBookIdAsync(long bookId);
    }
}
