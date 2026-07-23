using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Stationary:BaseEntity
{
    public string StationaryTitle { get; set; }
    public int StationaryPrice { get; set; }
    public string? StationaryDescription { get; set; }
}
