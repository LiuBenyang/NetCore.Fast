using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Fast.Infrastructure.ExceptionFilter.Dto;
using NetCore.Fast.Utility.Common;
using System.Net;

namespace NetCore.Fast.Infrastructure.ExceptionFilter
{
    public class CustomExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 写入日志
            UseLog.Error(context.Exception.Message, context.Exception);

            //处理各种异常
            context.ExceptionHandled = true;
            context.Result = new CustomExceptionResult(context.Exception);
        }
    }
}
