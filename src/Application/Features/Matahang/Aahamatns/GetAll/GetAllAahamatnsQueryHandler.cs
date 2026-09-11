using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Matahang.Aahamatns.GetAll;

public sealed class GetAllAahamatnsQueryHandler(
    IAppDbContext db,
    ICurrentUser user
) : IQueryHandler<GetAllAahamatnsQuery, Result<AahamatnsResponse>> {
    public async Task<Result<AahamatnsResponse>> HandleAsync(GetAllAahamatnsQuery query, CancellationToken cancellationToken = default) {
        var aahamatns = await db.Aahamatns
            .Where(x => x.OwnerId == user.UserIdGuid)
            .Select(x => AahamatnMinimalResponse.FromDomain(x))
            .ToListAsync(cancellationToken);

        return Result.Success(new AahamatnsResponse(aahamatns));
    }
}
