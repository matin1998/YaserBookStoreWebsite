using BookStore.Application.DTOs.Account;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Services
{
    public class AccountService: IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }
        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(long id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<EditProfileDTO> GetProfileAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            return new EditProfileDTO
            {
                FullName = user.FullName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<IdentityResult> UpdateProfileAsync(long userId,EditProfileDTO model)
        {
            var duplicateUser = await _userManager.FindByEmailAsync(model.Email);

            if (duplicateUser != null && duplicateUser.Id != userId)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "این ایمیل قبلاً ثبت شده است."
                    });
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return IdentityResult.Failed();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(long userId, ChangePasswordDTO model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return IdentityResult.Failed();

            return await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword);
        }
    }
}
