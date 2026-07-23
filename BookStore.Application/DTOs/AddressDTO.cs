using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs;

public class AddressDTO
{
    public long Id { get; set; }

    [Required]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Required]
    [Display(Name = "نام گیرنده")]
    public string FullName { get; set; }

    [Required]
    [Display(Name = "شماره موبایل")]
    [MaxLength(11)]
    public string Mobile { get; set; }

    [Required]
    [Display(Name = "استان")]
    public string Province { get; set; }

    [Required]
    [Display(Name = "شهر")]
    public string City { get; set; }

    [Required]
    [Display(Name = "کد پستی")]
    [MaxLength(10)]
    public string PostalCode { get; set; }

    [Required]
    [Display(Name = "آدرس")]
    public string AddressText { get; set; }
    [Display(Name = "آدرس پیشفرض")]
    public bool IsDefault { get; set; }
}
