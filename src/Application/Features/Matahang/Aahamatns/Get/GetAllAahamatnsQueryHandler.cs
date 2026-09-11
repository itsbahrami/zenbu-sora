using App.Application.Abstractions.Data;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Matahang.Aahamatns.Get;

public sealed class GetAahamatnQueryHandler(
    IAppDbContext db,
    ICurrentUser user
) : IQueryHandler<GetAahamatnQuery, Result<AahamatnFullResponse>> {
    public async Task<Result<AahamatnFullResponse>> HandleAsync(GetAahamatnQuery query, CancellationToken cancellationToken = default) {
        var aahamatn = await db.Aahamatns
            .FirstOrDefaultAsync(a => a.Id == query.Id && a.OwnerId == user.UserIdGuid, cancellationToken);

        if (aahamatn is null) {
            return (Result<AahamatnFullResponse>)Result.Failure(Error.NotFound(
                "Aahamatn.NotFound",
                $"Aahamatn with ID '{query.Id}' was not found or you do not have access."
            ));
        }

        return Result.Success(AahamatnFullResponse.FromDomain(aahamatn));
    }
}
