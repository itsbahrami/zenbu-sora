using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Jot.Jottings.GetAll;

public sealed record GetAllJottingsQuery : IQuery<Result<List<JottingResponse>>>;
