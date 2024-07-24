using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Extensions;
using E_Commerce.APIs.Helpers;
using E_Commerce.APIs.MiddleWares;
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
			builder.Services.AddSwaggerServices();
			builder.Services.AddDbContext<StoreDbContext>(options =>

			   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))
			   );
			
			builder.Services.AddApplicationServices(builder.Configuration);
			//builder.Services.AddAutoMapper(cfg =>
			//{
			//	cfg.AddProfile(new MappingProfiles(builder.Configuration));
			//});
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
			app.UseMiddleware<ExceptionMiddleWare>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlWares();
			}
			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();
		


			app.Run();
		}
	}
}
