using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.AdminSide.Book
{
    public class EditBookDTO
    {
        public int Id { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public int BookPrice { get; set; }
        public string? BookDescription { get; set; }
        [Required]
        public int BookInventory { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
