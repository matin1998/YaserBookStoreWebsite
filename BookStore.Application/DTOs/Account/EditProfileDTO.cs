using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Account;

public class EditProfileDTO
{
    [Required(ErrorMessage = "نام و نام خانوادگی الزامی است.")]
    [Display(Name = "نام و نام خانوادگی")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "ایمیل الزامی است.")]
    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست.")]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "شماره موبایل معتبر نیست.")]
    [Display(Name = "شماره موبایل")]
    public string? PhoneNumber { get; set; }
}
