using AspNetCoreGeneratedDocument;
using BookStore.Application.DTOs.AdminSide.Books;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ImageController : AdminBaseController
    {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        public ImageController(IBookService bookService, IImageService imageService)
        {
            _bookService = bookService;
            _imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> BookImages(long bookId)
        {
            var book =await _bookService.GetABookByIdAsync(bookId);
            var images = await _imageService.GetImagesByBookIdAsync(bookId);
            var model = new BookImagesDTO
            {
                BookId = book.Id,
                BookTitle = book.BookTitle,
                Images = images
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(BookImagesDTO model)
        {
            if (model.NewImages != null && model.NewImages.Any())
            {
                foreach (var image in model.NewImages)
                {
                    await _imageService.AddImageAsync(image, model.BookId);
                }
            }

            return RedirectToAction(nameof(BookImages),
                new { bookId = model.BookId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage (int imageId) 
        {
            var image = await _imageService.GetImageByIdAsync(imageId);

            if (image == null)
                return NotFound();

            long bookId = image.BookId;

            await _imageService.DeleteImageAsync(imageId);

            return RedirectToAction(nameof(BookImages),
                new { bookId });
        }
        
        public async  Task<IActionResult> SetMainImage (int imageId) 
        {
            
            var image = await _imageService.GetImageByIdAsync(imageId);

            if (image == null)
                return NotFound();

            long bookId = image.BookId;
            await _imageService.SetMainImageAsync(imageId);
            return RedirectToAction(nameof(BookImages),
                new { bookId });
        }

    }
}
