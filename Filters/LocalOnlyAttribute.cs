using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PortfolioService.Filters
{
    public class LocalOnlyAttribute : ActionFilterAttribute
    {
        private readonly int _allowedPort;

        public LocalOnlyAttribute()
        {
            _allowedPort = AppSettings.LocalPort;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var host = context.HttpContext.Request.Host.Value;
            var port = GetPortFromHost(host);

            if (port != _allowedPort)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(context);
        }

        private int GetPortFromHost(string host)
        {
            var parts = host.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out var port))
            {
                return port;
            }

            return -1;
        }
    }
}