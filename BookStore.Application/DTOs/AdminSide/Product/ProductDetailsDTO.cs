using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.AdminSide.Product;

public class ProductDetailsDTO
{
    public long Id { get; set; }
    public string Title { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public int Inventory { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();
}
