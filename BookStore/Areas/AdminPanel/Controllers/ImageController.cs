/*using AspNetCoreGeneratedDocument;*/
using BookStore.Application.DTOs.AdminSide.Product;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers
{
    public class ImageController : AdminBaseController
    {
        private readonly IImageService _imageService;
        private readonly IProductService _productService;
        public ImageController(IImageService imageService, IProductService productService)
        {
            _imageService = imageService;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductImages(long productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var images = await _imageService.GetImagesByProductIdAsync(productId);

            var model = new ProductImagesDTO
            {
                ProductId = product.Id,
                ProductTitle = product.Title,
                Images = images
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImages(ProductImagesDTO model)
        {
            if (model.NewImages != null && model.NewImages.Any())
            {
                foreach (var image in model.NewImages)
                {
                    await _imageService.AddImageAsync(image, model.ProductId);
                }
            }

            return RedirectToAction(nameof(ProductImages),
                new { productId = model.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage (int imageId) 
        {
            var image = await _imageService.GetImageByIdAsync(imageId);

            if (image == null)
                return NotFound();

            long productId = image.ProductId;

            await _imageService.DeleteImageAsync(imageId);

            return RedirectToAction(nameof(ProductImages),
                 new { productId });
        }
        
        public async  Task<IActionResult> SetMainImage (int imageId) 
        {
            
            var image = await _imageService.GetImageByIdAsync(imageId);

            if (image == null)
                return NotFound();

            long productId = image.ProductId;
            await _imageService.SetMainImageAsync(imageId);
            return RedirectToAction(nameof(ProductImages),
                 new { productId });
        }

    }
}
