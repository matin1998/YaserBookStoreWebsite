using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class Address:BaseEntity
{

    public long UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(150)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(11)]
    public string Mobile { get; set; }

    [Required]
    [MaxLength(50)]
    public string Province { get; set; }

    [Required]
    [MaxLength(50)]
    public string City { get; set; }

    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(1000)]
    public string AddressText { get; set; }

    public bool IsDefault { get; set; }
}
