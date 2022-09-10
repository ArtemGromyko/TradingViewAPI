using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TradingView.Domain.Exceptions;
internal class ApiError
{
    public ApiErrorCode Code { get; set; }

    public string Message { get; set; }

    public string CorrelationId { get; set; }

    public Dictionary<string, object> Properties { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
