using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers;
using E_Commerce.Core.Repository.Contract;
using E_Commerce.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Extensions
{
	public static class AddApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration? configuration)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			#region This dependences on configuration
			if (configuration is not null)
			{
				services.AddAutoMapper(cfg =>
				{
					cfg.AddProfile(new MappingProfiles(configuration));
				});
			} 
			#endregion

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														.SelectMany(p => p.Value.Errors)
														.Select(E => E.ErrorMessage)
														.ToList();
					var response = new ApiResponseValidation()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);

				};

			}
			);
			return services;
		}
	}
}
