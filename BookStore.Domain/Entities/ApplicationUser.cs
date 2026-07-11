using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public string FullName { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

