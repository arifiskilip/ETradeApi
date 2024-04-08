using Serilog.Context;
using System.Security.Claims;

namespace ETradeApi.API.Middlewares
{
	public class LogUsernameMiddleware
	{
		private readonly RequestDelegate _next;

		public LogUsernameMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.User.Identity.IsAuthenticated)
			{
				LogContext.PushProperty("Username", context.User.Identity.Name);
			}
			await _next(context);
		}
	}

	public static class LogUsernameMiddlewareExtensions
	{
		public static IApplicationBuilder UseLogUserNameMiddlaware(this IApplicationBuilder app)
		{
			return app.UseMiddleware<LogUsernameMiddleware>();
		}
	}
}
