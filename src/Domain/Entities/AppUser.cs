using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public required string DisplayName { get; set; }

    // Navigation property
    public List<Order> Orders { get; set; } = [];
}
