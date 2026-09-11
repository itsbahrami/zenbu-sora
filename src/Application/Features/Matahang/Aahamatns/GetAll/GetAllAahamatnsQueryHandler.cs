using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Matahang.Aahamatns.GetAll;

public sealed class GetAllAahamatnsQueryHandler(
    IAppDbContext db
) : IQueryHandler<GetAllAahamatnsQuery, Result<List<AahamatnResponse>>> {
    public async Task<Result<List<AahamatnResponse>>> HandleAsync(GetAllAahamatnsQuery query, CancellationToken cancellationToken = default) {
        var aahamatns = await db.Aahamatns
            .Select(x => AahamatnResponse.FromDomain(x))
            .ToListAsync(cancellationToken);

        return Result.Success(aahamatns);
    }
}
