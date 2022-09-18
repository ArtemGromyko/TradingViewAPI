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
}
