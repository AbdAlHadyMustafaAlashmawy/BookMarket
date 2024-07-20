using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMarket.Filters
{
    public class ExceptionHandelAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new PartialViewResult() { ViewName= "ErrorHappened" };
        }
    }
}
