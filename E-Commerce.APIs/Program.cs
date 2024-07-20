using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers;
using E_Commerce.Core.Models;
using E_Commerce.Core.Repository.Contract;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreDbContext>(options =>

			   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))
			   );
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile(new MappingProfiles(builder.Configuration));
			});
			builder.Services.Configure<ApiBehaviorOptions>(options =>
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
			var app = builder.Build();
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var context = services.GetRequiredService<StoreDbContext>();
				var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			

				try
				{
					await context.Database.MigrateAsync();
					await StoreContextSeed.SeedAsync(context);

				}
				catch (Exception ex)
				{
					var logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex, "An error occurred while migrating the database.");
				}			
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
