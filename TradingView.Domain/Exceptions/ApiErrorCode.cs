using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingView.Domain.Exceptions;

public enum ApiErrorCode
{
    General = 1,
    ValidationFailed,
    NotFound,
    BadRequest,
    Unauthorized,
    Forbidden
}