using BookStore.Application.DTOs.Account;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterDTO model);
    Task<SignInResult> LoginAsync(LoginDTO model);
    Task LogoutAsync();
    Task<ApplicationUser?> GetUserByEmailAsync(string email);

    Task<ApplicationUser?> GetUserByIdAsync(long id);

    Task<IList<string>> GetRolesAsync(ApplicationUser user);

    Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

    Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);
}
