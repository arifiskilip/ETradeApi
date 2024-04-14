using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ETradeApi.API.Middlewares
{

	public static class ConfigureExceptionHandlerExtension
	{
		public static void ConfigureExtensionHandler(this WebApplication application)
		{
			application.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						await context.Response.WriteAsync(JsonSerializer.Serialize(new
						{
							StatusCode = context.Response.StatusCode,
							Message = contextFeature.Error.Message,
							Title = "Hata!"
						}));
					}
				});
			});
		}
	}
}
