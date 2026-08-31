using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models.Jot;

namespace App.Application.Features.Jot.Jottings.Create;

public sealed class CreateJottingCommandHandler(
    IAppDbContext db,
    ICurrentUser user
) : ICommandHandler<CreateJottingCommand, Result<JottingResponse>> {
    public async Task<Result<JottingResponse>> HandleAsync(CreateJottingCommand command, CancellationToken cancellationToken = default) {
        var newJotting = new Jotting {
            Content = command.Content,
            OwnerId = user.UserIdGuid,
            Title = command.Title,
        };

        db.Jottings.Add(newJotting);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(JottingResponse.FromDomain(newJotting));
    }
}
