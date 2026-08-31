using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Jot.Jottings.GetAll;

public sealed class GetAllJottingsQueryHandler(
    IAppDbContext db,
    ICurrentUser user
) : IQueryHandler<GetAllJottingsQuery, Result<List<JottingResponse>>> {
    public async Task<Result<List<JottingResponse>>> HandleAsync(GetAllJottingsQuery query, CancellationToken cancellationToken = default) {
        var jottings = await db.Jottings
            .Where(j => j.OwnerId == user.UserIdGuid)
            .Select(j => JottingResponse.FromDomain(j))
            .ToListAsync(cancellationToken);

        return Result.Success(jottings);
    }
}
