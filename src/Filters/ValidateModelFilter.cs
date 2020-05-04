using ApiBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiBase.Filters
{
    public class ValidateModelFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context) { }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new ValidationResult(context.ModelState));
            }
        }
    }
}
