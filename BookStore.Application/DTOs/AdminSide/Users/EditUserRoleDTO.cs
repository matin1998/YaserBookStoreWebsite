using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;





namespace BookStore.Application.DTOs.AdminSide.Users;
public class EditUserRoleDTO
    {
        public long UserId { get; set; }

        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        [Required]
        public string SelectedRole { get; set; } = "";

        public List<string> Roles { get; set; } = new();
}
