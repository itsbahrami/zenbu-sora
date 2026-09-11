using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Matahang.Aahamatns.Get;

public sealed record GetAahamatnQuery(Guid Id) : IQuery<Result<AahamatnFullResponse>>;
