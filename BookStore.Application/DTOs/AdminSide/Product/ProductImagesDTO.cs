using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.AdminSide.Product;

public class ProductImagesDTO
{
    public long ProductId { get; set; }
    public string ProductTitle { get; set; }
    public List<Image> Images { get; set; }

    public List<IFormFile> NewImages { get; set; }
        = new();
}
