using BookStore.Application.DTOs.AdminSide.Users;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations;

public class UserService:IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<List<UserListDTO>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var model = new List<UserListDTO>();

        foreach (var user in users)
        {
            var role = (await _userManager.GetRolesAsync(user))
                .FirstOrDefault();

            model.Add(new UserListDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                RoleName = role ?? "-"
            });
        }

        return model;
    }

    public async Task<EditUserRoleDTO?> GetUserRoleAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return null;

        var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        var roles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();

        return new EditUserRoleDTO
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? "",
            SelectedRole = currentRole ?? "User",
            Roles = roles
        };
    }

    public async Task<bool> ChangeUserRoleAsync(EditUserRoleDTO model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());

        if (user == null)
            return false;

        var currentRoles = await _userManager.GetRolesAsync(user);

        bool isAdmin = currentRoles.Contains("Admin");

        if (isAdmin && model.SelectedRole == "User")
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            if (admins.Count == 1)
            {
                return false;
            }
        }

        if (currentRoles.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
                return false;
        }

        var addResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);

        return addResult.Succeeded;
    }
}
