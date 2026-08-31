using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models;

public sealed class ApplicationUser : IdentityUser {
    public string? FullName { get; set; }
    public string? DisplayName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
}
