using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.Fast.Utility.Common;
using NetCore.Fast.Utility.ToExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Fast.Infrastructure.ExceptionFilter.Dto
{
    public class ValidationFailedResultModel : ResultModel
    {
        public ValidationFailedResultModel(ModelStateDictionary modelState)
        {
            Code = 422;
            Message = "参数不合法";
            InnerMassge = modelState.Keys
                        .SelectMany(key => modelState[key].Errors
                        .Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();

            UseLog.Error(InnerMassge.ToJson());
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
