using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Matahang.Aahamatns.Update;

public sealed class UpdateAahamatnCommandHandler(
    IAppDbContext db
) : ICommandHandler<UpdateAahamatnCommand, Result> {
    public async Task<Result> HandleAsync(
        UpdateAahamatnCommand command,
        CancellationToken cancellationToken = default
    ) {
        // Filter by BOTH Id AND OwnerId in one go
        var aahamatn = await db.Aahamatns
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);

        if (aahamatn is null) {
            return Result.Failure(Error.NotFound(
                "Aahamatn.NotFound",
                $"Aahamatn with ID '{command.Id}' was not found or you do not have access."
            ));
        }

        // Update properties
        aahamatn.Title = command.Title;
        aahamatn.Language = command.Language;
        aahamatn.Lyrics = command.Lyrics;
        aahamatn.Artist = command.Artist;
        aahamatn.Color = command.Color;
        aahamatn.AudioUrl = command.AudioUrl;
        aahamatn.SourceUrl = command.SourceUrl;
        aahamatn.MarkUpdated();

        db.Aahamatns.Update(aahamatn);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
