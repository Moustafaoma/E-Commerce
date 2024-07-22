using E_Commerce.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace E_Commerce.APIs.MiddleWares
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleWare> _logger;
		private readonly IWebHostEnvironment _env;
		public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IWebHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			
			try
			{
				await _next.Invoke(httpContext);
			}
			catch (Exception ex)
			{
				
					_logger.LogError(ex.Message);
					await HandleExceptionAsync(httpContext, ex);
			}
		}
		
		
		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var response = _env.IsDevelopment() ? new ApiResponseServerError((int)HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace) :
					new ApiResponseServerError((int)HttpStatusCode.InternalServerError);
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
		}

	}
}
