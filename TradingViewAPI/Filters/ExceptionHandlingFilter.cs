using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TradingView.Models.Exceptions;

namespace TradingViewAPI.Filters;

public class ExceptionHandlingFilter : IAsyncExceptionFilter
{
    //private readonly ILogger _logger;

    public ExceptionHandlingFilter()//ILogger logger)
    {
        // _logger = logger;
    }
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        //_logger.Error(context.Exception);

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        var ex = context.Exception as ApiException;
        var apiError = new ApiErrorResponse { Message = context.Exception.Message, Code = (ApiErrorCode)(int)ex.Code };

        switch (context.Exception)
        {
            case ValidationException:
                statusCode = HttpStatusCode.BadRequest;
                apiError.Code = ApiErrorCode.ValidationFailed;
                break;
            case ApiException:
                statusCode = HttpStatusCode.BadRequest;
                apiError.Code = (ApiErrorCode)(int)ex.Code;
                break;
        }

        var result = JsonConvert.SerializeObject(apiError);

        context.Result = new ContentResult
        {
            StatusCode = (int?)statusCode,
            ContentType = "application/json;charset=utf-8",
            Content = result
        };
        context.ExceptionHandled = true;
    }
}