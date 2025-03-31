using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _ratelimitwindow = TimeSpan.FromSeconds(30);
        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env, IMemoryCache memoryCache)
        {
            _next = next;
            _env = env;
            _memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                applysecurity(Context);
                if (!IsRequestAllow(Context))
                {
                    await WriteResponse(Context, HttpStatusCode.TooManyRequests);
                }
                await _next(Context);

            }
            catch (Exception ex)
            {
                await WriteResponse(Context, HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);

            }
        }
        //Rate Limit By ip address for 30 seconds and 8 request
        private bool IsRequestAllow(HttpContext Context)
        {
            var Ip = Context.Connection.RemoteIpAddress!.ToString();
            var Cache = $"Rate {Ip}";
            var DateNow = DateTime.Now;
            var (Timestamp, Count) = _memoryCache.GetOrCreate(Cache, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _ratelimitwindow;
                return (DateNow, 0);
            });
            if (DateNow - Timestamp < _ratelimitwindow)
            {
                if (Count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(Cache, (Timestamp, Count + 1), _ratelimitwindow);
            }
            else
            {
                _memoryCache.Set(Cache, (Timestamp, 0), _ratelimitwindow);
            }
            return true;

        }
        // write response for error
        private async Task WriteResponse(HttpContext Context, HttpStatusCode httpStatus, string? message = null, string? stackTrace = null)
        {
            Context.Response.StatusCode = (int)httpStatus;
            Context.Response.ContentType = "application/json";
            var response = _env.IsDevelopment() ? new ApiException((int)httpStatus, message, stackTrace) : new ApiException((int)httpStatus, message);
            var json = JsonSerializer.Serialize(response);
            await Context.Response.WriteAsync(json);
        }
        // prevent xss attack and click jacking
        private void applysecurity(HttpContext Context)
        {
            Context.Response.Headers["X-Frame-Options"] = "Deny";
            Context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            Context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        }
    }
}
