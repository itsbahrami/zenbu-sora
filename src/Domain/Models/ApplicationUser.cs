using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models;

public sealed class ApplicationUser : IdentityUser {
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
}
