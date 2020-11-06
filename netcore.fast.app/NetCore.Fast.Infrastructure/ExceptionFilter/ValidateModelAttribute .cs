using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Fast.Infrastructure.ExceptionFilter.Dto;

namespace NetCore.Fast.Infrastructure.ExceptionFilter
{
    /// <summary>
    /// 模型验证过滤器
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }

    }
}
