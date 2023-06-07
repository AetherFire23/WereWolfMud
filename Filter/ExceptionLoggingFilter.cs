using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace WereWolfMud.Filter
{
    public class ExceptionLoggingFilter : IHubFilter
    {
        private readonly ILogger<ExceptionLoggingFilter> _logger;

        public ExceptionLoggingFilter(ILogger<ExceptionLoggingFilter> logger)
        {
            _logger = logger;
        }

        public async Task<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, Task<object>> next)
        {
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                // Log the exception
                _logger.LogError(ex, "An exception occurred during SignalR hub method invocation.");

                // You can choose to re-throw the exception or return a specific result as needed.
                // If you re-throw the exception, it will be propagated to the client.
                // throw;

                // Alternatively, you can return a specific result to the client.
                // For example:
                // return new HubExceptionResult("An error occurred during the hub method invocation.");
            }
            return null; // If no exception occurs and no other result is returned, return null.
        }
    }
}
