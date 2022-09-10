namespace TradingView.Models.Exceptions;
public static class ApiExceptionExtensions
{
    private const string ErrorMsgUnknownError = "Unknown error";

    public static ApiException Create(this ApiException item, HttpResponseMessage response)
    {
        return new ApiException(response.ReasonPhrase)
        {
            ErrorMessage = ErrorMsgUnknownError,
            Code = (ApiErrorCode)response.StatusCode,
        };
    }

    public static ApiException Create(this ApiException item, Exception ex)
    {
        ApiException result = new ApiException
        {
            ErrorMessage = ex.Message,
            Code = ApiErrorCode.General
        };

        return result;
    }

    public static ApiException Create(this ApiException item, ApiError apiError)
    {
        if (apiError == null)
        {
            return new ApiException
            {
                Code = ApiErrorCode.General,
                ErrorMessage = ErrorMsgUnknownError
            };
        }

        return new ApiException(apiError.Message)
        {
            Code = apiError.Code,
            ErrorMessage = apiError.ToString()
        };
    }
}

