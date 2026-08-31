using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Jot.Jottings.Update;

public sealed class UpdateJottingCommandHandler(
    IAppDbContext db,
    ICurrentUser user
) : ICommandHandler<UpdateJottingCommand, Result> {
    public async Task<Result> HandleAsync(
        UpdateJottingCommand command,
        CancellationToken cancellationToken = default
    ) {
        // Filter by BOTH Id AND OwnerId in one go
        var jotting = await db.Jottings
            .FirstOrDefaultAsync(j => j.Id == command.Id && j.OwnerId == user.UserIdGuid, cancellationToken);

        if (jotting is null) {
            return Result.Failure(Error.NotFound(
                "Jotting.NotFound",
                $"Jotting with ID '{command.Id}' was not found or you do not have access."
            ));
        }

        // Update properties
        jotting.Title = command.Title;
        jotting.Content = command.Content;
        jotting.MarkUpdated(); // Your domain method

        db.Jottings.Update(jotting); // Optional, EF tracks it anyway
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
