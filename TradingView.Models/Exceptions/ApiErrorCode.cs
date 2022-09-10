namespace TradingView.Models.Exceptions;

public enum ApiErrorCode
{
    General = 1,
    ValidationFailed,
    NotFound,
    BadRequest,
    Unauthorized,
    Forbidden
}