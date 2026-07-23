using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Account;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "رمز عبور فعلی الزامی است.")]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور فعلی")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "رمز عبور جدید الزامی است.")]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور جدید")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword),
        ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند.")]
    [Display(Name = "تکرار رمز عبور")]
    public string ConfirmPassword { get; set; }
}
