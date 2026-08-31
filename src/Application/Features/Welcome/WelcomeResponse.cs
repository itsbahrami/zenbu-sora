namespace App.Application.Features.Welcome;

public sealed record WelcomeResponse {
    public required string Message { get; init; }
}
