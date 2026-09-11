using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models.Matahang;

namespace App.Application.Features.Matahang.Aahamatns.Create;

public sealed class CreateAahamatnCommandHandler(
    IAppDbContext db
) : ICommandHandler<CreateAahamatnCommand, Result<AahamatnResponse>> {
    public async Task<Result<AahamatnResponse>> HandleAsync(CreateAahamatnCommand command, CancellationToken cancellationToken = default) {
        var newAahamatn = new Aahamatn {
            Language = command.Language,
            Lyrics = command.Lyrics,
            Artist = command.Artist,
            AudioUrl = command.AudioUrl,
            Color = command.Color,
            SourceUrl = command.SourceUrl,
            Title = command.Title,
        };

        db.Aahamatns.Add(newAahamatn);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(AahamatnResponse.FromDomain(newAahamatn));
    }
}
