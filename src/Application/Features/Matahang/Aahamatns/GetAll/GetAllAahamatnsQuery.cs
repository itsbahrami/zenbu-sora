using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Matahang.Aahamatns.GetAll;

public sealed record GetAllAahamatnsQuery : IQuery<Result<AahamatnsResponse>>;
