using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.AdminSide.Books;

public class BookImagesDTO
{
    public int BookId { get; set; }

    public string BookTitle { get; set; }

    public List<Image> Images { get; set; }

    public List<IFormFile> NewImages { get; set; }
        = new();
}
