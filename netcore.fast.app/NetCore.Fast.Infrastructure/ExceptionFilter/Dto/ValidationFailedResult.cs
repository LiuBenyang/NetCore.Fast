using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetCore.Fast.Infrastructure.ExceptionFilter.Dto
{
    public  class ValidationFailedResult: ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
          : base(new ValidationFailedResultModel(modelState))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
