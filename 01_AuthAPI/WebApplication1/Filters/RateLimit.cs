using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AuthAPI.Services;
namespace AuthAPI.Filters
{
    // Kế thừa ActionFilterAttribute
    public class RateLimit : ActionFilterAttribute
    {
        private readonly int _maxRequest;
        private readonly int _timeLimit;
        public RateLimit (int maxRequest, int timeLimit)
        {
            _maxRequest = maxRequest;
            _timeLimit = timeLimit;
        }

        // Pre-processing filter
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Định vị Service
            var limitServices = context.HttpContext.RequestServices.GetService<RateLimitServices>();
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknow";
            // Gọi hàm bên RateLimitServices
            if (limitServices != null && !limitServices.IsAllowed(ipAddress, _maxRequest, TimeSpan.FromSeconds(_timeLimit)))
            {
                // Short-circuiting
                context.Result = new ObjectResult(new { message = "Thử lại sau vài giây" })
                {
                    StatusCode = StatusCodes.Status429TooManyRequests,
                };
            }
            base.OnActionExecuting(context);
        }
    }
}