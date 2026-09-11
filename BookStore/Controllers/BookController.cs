using BookStore.Application.DTOs.AdminSide.Product;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    public class BookController : Controller
    {
        
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        public BookController (ICategoryService categoryService , IProductService productService, IBookService bookService, IImageService imageService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _bookService = bookService;
            _imageService = imageService;
        }
        // GET: BookController
        public ActionResult Categories()
        {
            var model = _categoryService.GetListOFCategories();
            return View(model);
        }
        public ActionResult Category(int categoryId)
        {
            //باید کتاب های یک دسته خاص را باز کند
            var books= _bookService.GetListOfBooksByCategoryId(categoryId);
            return View(books);
        }
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            var images = await _imageService.GetImagesByProductIdAsync(productId);
            var model = new ProductDetailsDTO() {
             Id = product.Id
            ,Title=product.Title
            , Description=product.Description
            ,Price=product.Price
            ,Inventory=product.Inventory
            ,Images=images};
            return View(model);
        }
    }
}
