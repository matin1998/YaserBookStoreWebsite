using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.AdminSide.Users;

public class UserListDTO
{
    public long Id { get; set; }

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string RoleName { get; set; } = "";
}
