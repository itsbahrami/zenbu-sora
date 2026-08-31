namespace App.Application.Abstractions.Identity;

public interface ICurrentUser {
    string? UserId { get; }
    Guid UserIdGuid => Guid.Parse(UserId ?? "");
    string? Email { get; }
    bool IsAuthenticated { get; }
}
