using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingView.Domain.Exceptions;
internal class ApiExceptionExtensions
{
    private const string ErrorMsgUnknownError = "Unknown error";

    public static ApiException Create(this ApiException item, HttpResponseMessage response)
    {
        string stringResponse = response.Content.ReadAsStringAsync().Result;
        ApiException result = null;
        try
        {
            result = JsonConvert.DeserializeObject<ApiException>(stringResponse);
        }
        catch { }

        if (result == null)
        {
            result = new ApiException
            {
                ErrorMessage = ErrorMsgUnknownError,
                Code = ApiErrorCode.General
            };
        }

        return result;
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
        List<PropertyError> properties = null;
        try
        {
            var propertiesString = apiError.Properties.FirstOrDefault().Value.ToString();
            properties = JsonConvert.DeserializeObject<List<PropertyError>>(propertiesString);
        }
        catch { }

        if (apiError == null)
        {
            return new ApiException
            {
                Code = ApiErrorCode.General,
                ErrorMessage = ErrorMsgUnknownError
            };
        }

        if (properties == null)
        {
            return new ApiException(apiError.Message)
            {
                Code = (ApiErrorCode)apiError.Code,
                ErrorMessage = apiError.ToString()
            };
        }

        return new ApiException(properties.FirstOrDefault()?.Message)
        {
            Code = (ApiErrorCode)apiError.Code,
            ErrorMessage = apiError.Message,
            Properties = apiError.Properties
        };
    }
}
