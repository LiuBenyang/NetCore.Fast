using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.ExceptionFilter.Dto
{
    public class CustomExceptionResultModel : ResultModel
    {
        public CustomExceptionResultModel(Exception exception)
        {
            Code = 500;
            Message = "发生系统级别异常";
            InnerMassge = exception.InnerException != null ?
                exception.InnerException.Message :
                exception.Message;
            //Result = exception.Message;
        }
    }
}
