using BookStore.Application.DTOs.AdminSide.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface IUserService
{
    Task<List<UserListDTO>> GetUsersAsync();

    Task<EditUserRoleDTO?> GetUserRoleAsync(long userId);

    Task<bool> ChangeUserRoleAsync(EditUserRoleDTO model);
}
