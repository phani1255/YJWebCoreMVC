using System.Collections.Concurrent;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Middleware
{
    public class LicenseMiddleware
    {
        private static readonly ConcurrentDictionary<string, int> ActiveUsers = new();
        private readonly RequestDelegate _next;
        private readonly HelperService _helperService;

        public LicenseMiddleware(RequestDelegate next, HelperService helperService)
        {
            _next = next;
            _helperService = helperService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var licenseKey = _helperService.HelperCommon.LicenseKey;

            if (string.IsNullOrEmpty(licenseKey))
            {
                context.Response.Redirect("/Unauthorized");
                return;
            }

            ActiveUsers.AddOrUpdate(licenseKey, 1, (_, count) => count + 1);

            int maxUsers = _helperService.HelperCommon.GetMaxUsersForLicense();

            if (ActiveUsers[licenseKey] > maxUsers)
            {
                ActiveUsers.AddOrUpdate(licenseKey, 1, (_, count) => count - 1);
                context.Response.Redirect("/LicenseLimitExceeded");
                return;
            }

            await _next(context);

            // Session_End equivalent
            ActiveUsers.AddOrUpdate(licenseKey, 0, (_, count) => Math.Max(count - 1, 0));
        }
    }
}
