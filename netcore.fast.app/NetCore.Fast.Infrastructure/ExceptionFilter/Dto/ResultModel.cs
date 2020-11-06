using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.ExceptionFilter.Dto
{
    public class ResultModel
    {

        public ResultModel(int code = 200, string message = null, object innerMassge = null,
        object result = null)
        {
            this.Code = code;
            this.Result = result;
            this.InnerMassge = innerMassge;
            this.Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public object InnerMassge { get; set; }

        public object Result { get; set; }
    }
}
