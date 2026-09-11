using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Matahang.Aahamatns.Delete;

public sealed class DeleteAahamatnCommandHandler(
    IAppDbContext db
) : ICommandHandler<DeleteAahamatnCommand, Result> {
    public async Task<Result> HandleAsync(
        DeleteAahamatnCommand command,
        CancellationToken cancellationToken = default
    ) {
        // Filter by BOTH Id AND OwnerId
        var aahamatn = await db.Aahamatns
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (aahamatn is null) {
            return Result.Failure(Error.NotFound(
                "Aahamatn.NotFound",
                $"Aahamatn with ID '{command.Id}' was not found or you do not have access."
            ));
        }

        db.Aahamatns.Remove(aahamatn);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
