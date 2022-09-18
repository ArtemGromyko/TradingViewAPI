namespace TradingView.Models.Exceptions;
public static class ApiExceptionExtensions
{
    private const string ErrorMsgUnknownError = "Unknown error";

    public static ApiException Create(this ApiException item, HttpResponseMessage response)
    {
        return new ApiException(response.ReasonPhrase!)
        {
            Code = (ApiErrorCode)response.StatusCode,
        };
    }
}

