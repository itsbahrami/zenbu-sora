using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models.Matahang;

namespace App.Application.Features.Matahang.Aahamatns.Create;

public sealed class CreateAahamatnCommandHandler(
    IAppDbContext db,
    ICurrentUser user
) : ICommandHandler<CreateAahamatnCommand, Result<AahamatnFullResponse>> {
    public async Task<Result<AahamatnFullResponse>> HandleAsync(CreateAahamatnCommand command, CancellationToken cancellationToken = default) {
        var newAahamatn = new Aahamatn {
            OwnerId = user.UserIdGuid,
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

        return Result.Success(AahamatnFullResponse.FromDomain(newAahamatn));
    }
}
