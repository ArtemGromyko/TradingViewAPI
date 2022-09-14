using System.Text.Json;

namespace TradingView.Models.Exceptions;
public class ApiError
{
    public ApiErrorCode Code { get; set; }

    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
