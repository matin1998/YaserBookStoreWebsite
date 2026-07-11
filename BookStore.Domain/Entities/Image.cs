using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Image
{
    public int Id { get; set; }

    public string ImageName { get; set; }

    public bool IsMainImage { get; set; }

    public int BookId { get; set; }

    public Book Book { get; set; }
}
