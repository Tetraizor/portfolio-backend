using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PortfolioService.Filters;

public class LocalOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers;
        var host = headers["Host"].ToString();

        if (!host.Contains("localhost"))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}