namespace TradingView.Models.Exceptions;

public enum ApiErrorCode
{
    General = 1,
    ValidationFailed,
    NotFound = 404,
    BadRequest = 400,
    Unauthorized,
    Forbidden
}