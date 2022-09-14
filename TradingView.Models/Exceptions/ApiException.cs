namespace TradingView.Models.Exceptions;
public class ApiException : Exception
{
    public ApiException()
    {
    }

    public ApiException(string Message) : base(Message)
    {
    }

    public ApiErrorCode Code { get; set; }
    public string? ErrorMessage { get; set; }

    public override string ToString()
    {
        return $"Http Error Code: {Code}; Error message: {ErrorMessage}";
    }
}
