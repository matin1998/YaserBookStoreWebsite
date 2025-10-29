using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string CategoryTitle { get; set; }
    public ICollection<Book> books { get; set; } = new List<Book>();
}
