using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PortfolioService.Filters
{
    public class LocalOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            // If the X-Forwarded-For header is empty, use the remote IP address directly
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                // If no IP address is found, block the request
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the IP address is local
            if (IsLocalIpAddress(ipAddress))
            {
                // Allow the request to proceed
                base.OnActionExecuting(context);
            }
            else
            {
                // Block the request by returning a 401 Unauthorized status
                context.Result = new UnauthorizedResult();
            }
        }

        private bool IsLocalIpAddress(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out IPAddress? address))
            {
                // If it's an IPv6-mapped IPv4 address (e.g., "::ffff:192.168.1.10")
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    if (address.ToString().StartsWith("::ffff:"))
                    {
                        // Extract the IPv4 address part and check if it's private
                        var ipv4Address = address.MapToIPv4();
                        return IsPrivate(ipv4Address);
                    }
                }

                // If it's not an IPv6-mapped IPv4 address, check if it's IPv4 and check if it's private or loopback
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return IsPrivate(address) || IsLoopback(address);
                }
            }
            return false;
        }

        private bool IsPrivate(IPAddress address)
        {
            var bytes = address.GetAddressBytes();
            return bytes[0] == 10 ||
                   (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) ||
                   (bytes[0] == 192 && bytes[1] == 168);
        }

        private bool IsLoopback(IPAddress address)
        {
            return IPAddress.IsLoopback(address);
        }

    }
}