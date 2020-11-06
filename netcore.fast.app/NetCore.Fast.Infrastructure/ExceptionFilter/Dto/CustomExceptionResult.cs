using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.ExceptionFilter.Dto
{
    public class CustomExceptionResult : ObjectResult
    {
        public CustomExceptionResult(Exception exception)
            : base(new CustomExceptionResultModel(exception))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
