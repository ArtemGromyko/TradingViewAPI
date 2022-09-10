using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingView.Domain.Exceptions;
internal class ApiException : Exception
{
    public ApiException()
    {
    }

    public ApiException(string Message) : base(Message)
    {
    }

    public ApiErrorCode Code { get; set; }
    public string ErrorMessage { get; set; }

    public Dictionary<string, object> Properties { get; set; }

    public override string ToString()
    {
        return $"Http Error Code: {Code}; Error message: {ErrorMessage}";
    }

    //public enum ApiErrorCode
    //{
    //    General = 1,
    //    ValidationFailed = 2,
    //    NotFound = 3
    //}
}
