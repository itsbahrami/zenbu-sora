using App.Api.Extensions;
using App.Domain.Common;

namespace App.Api.Endpoints;

public static class EndpointUtils {
    public static IResult AutoResolveOk(Result result)
        => result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblemDetails();

    public static IResult AutoResolveOk<T>(Result<T> result)
        => result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : result.ToProblemDetails();

    public static IResult AutoResolveNoContent(Result result)
        => result.IsSuccess
            ? TypedResults.NoContent()
            : result.ToProblemDetails();

    public static IResult AutoResolveCreatedAt<T>(
        Result<T> result,
        string routeName,
        Func<T, object> routeValuesFactory
    ) => result.IsSuccess
        ? TypedResults.CreatedAtRoute(
            result.Value,
            routeName,
            routeValuesFactory(result.Value!)
        )
        : result.ToProblemDetails();
}
