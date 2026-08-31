using App.Domain.Common;

namespace App.Api.Extensions;

public static class ResultExtensions {
    public static IResult ToProblemDetails(this Result result) {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert a successful result to a problem.");

        var (statusCode, title) = result.Error!.Type switch {
            ErrorType.NotFound => (StatusCodes.Status404NotFound, "Not Found"),
            ErrorType.Validation => (StatusCodes.Status400BadRequest, "Validation Error"),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflict"),
            _ => (StatusCodes.Status500InternalServerError, "Server Error")
        };

        return Results.Problem(
            detail: result.Error.Message,
            statusCode: statusCode,
            title: title,
            extensions: new Dictionary<string, object?> {
                ["errorCode"] = result.Error.Code
            }
        );
    }
}
