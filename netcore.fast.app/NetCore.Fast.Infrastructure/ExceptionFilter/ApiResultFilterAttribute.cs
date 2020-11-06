using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Fast.Infrastructure.ExceptionFilter.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.ExceptionFilter
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ValidationFailedResult)
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = objectResult;
            }
            else
            {
                var objectResult = context.Result as ObjectResult;
                var result = new ResultModel(code: 200, message: "操作成功", result: null);

                if (context.Result is OkObjectResult)
                {
                    // 如果结果为字符串 则视为提示消息
                    if (objectResult.Value.GetType().Name.ToLower().Equals("string"))
                        result.Message = objectResult.Value.ToString();
                    else
                        result.Result = objectResult.Value;
                }
                else if (context.Result is EmptyResult)
                {
                    result.Message = "返回结果为空";
                }
                else if (context.Result is BadRequestObjectResult)
                {
                    result.Code = 400;
                    result.Message = objectResult.Value.ToString();
                }
                else if (context.Result is ObjectResult)
                {
                    // 如果结果为字符串 则视为提示消息
                    if (objectResult.Value.GetType().Name.ToLower().Equals("string"))
                        result.Message = objectResult.Value.ToString();
                    else
                        result.Result = objectResult.Value;
                }


                context.Result = new OkObjectResult(result);
            }
        }
    }
}
