namespace E_Commerce.APIs.Extensions
{
	public static class AddSwaggerServicesExtensions
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}
		public static WebApplication UseSwaggerMiddlWares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
