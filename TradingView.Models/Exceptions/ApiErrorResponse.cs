namespace TradingView.Models.Exceptions;
public class ApiErrorResponse
{
    public ApiErrorCode Code { get; set; }
    public string Message { get; set; }
}
