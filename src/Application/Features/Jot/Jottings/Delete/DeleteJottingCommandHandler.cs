using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Jot.Jottings.Delete;

public sealed class DeleteJottingCommandHandler(
    IAppDbContext db,
    ICurrentUser user
) : ICommandHandler<DeleteJottingCommand, Result> {
    public async Task<Result> HandleAsync(
        DeleteJottingCommand command,
        CancellationToken cancellationToken = default
    ) {
        // Filter by BOTH Id AND OwnerId
        var jotting = await db.Jottings
            .FirstOrDefaultAsync(j => j.Id == command.Id && j.OwnerId == user.UserIdGuid, cancellationToken);

        if (jotting is null) {
            return Result.Failure(Error.NotFound(
                "Jotting.NotFound",
                $"Jotting with ID '{command.Id}' was not found or you do not have access."
            ));
        }

        db.Jottings.Remove(jotting);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
