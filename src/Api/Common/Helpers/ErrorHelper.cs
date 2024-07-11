using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Application.Common;

namespace Api.Common.Helpers;

public static class ErrorHelper
{
    public static ProblemDetails GenerateProblem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return GetValidationProblem(errors);
        }

        if (errors.Count is 0)
        {
            return new ProblemDetails();
        }

        return GenerateProblem(errors[0]);
    }

    private static ProblemDetails GenerateProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var type = error.Type switch
        {
            ErrorType.Conflict => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8",
            ErrorType.NotFound => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
            _ => "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
        };

        return new ProblemDetails
        {
            Status = statusCode,
            Title = error.Description,
            Type = type,
        };
    }

    private static ValidationProblemDetails GetValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return new ValidationProblemDetails(modelStateDictionary)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "One or more validation errors occured"
        };
    }
}