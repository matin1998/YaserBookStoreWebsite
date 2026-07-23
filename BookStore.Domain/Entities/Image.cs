using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Image:BaseEntity
{
    public string ImageName { get; set; }

    public bool IsMainImage { get; set; }

    public long BookId { get; set; }

    public Book Book { get; set; }
}
