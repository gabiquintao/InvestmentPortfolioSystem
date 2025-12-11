// =============================================
// IPS.Data/Extensions/ServiceExtensions.cs
// Description: Extension methods for dependency injection
// =============================================

using IPS.Core.Interfaces;
using IPS.Data.Context;
using IPS.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IPS.Data.Extensions
{
	/// <summary>
	/// Extension methods for registering data layer services
	/// </summary>
	public static class ServiceExtensions
	{
		/// <summary>
		/// Registers data layer services in the DI container
		/// </summary>
		/// <param name="services">Service collection</param>
		/// <param name="configuration">Application configuration</param>
		/// <returns>Service collection for chaining</returns>
		public static IServiceCollection AddDataLayer(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			// Register DbContext
			services.AddDbContext<IPSDbContext>(options =>
			{
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					sqlOptions =>
					{
						// Enable retry on failure
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 3,
							maxRetryDelay: TimeSpan.FromSeconds(5),
							errorNumbersToAdd: null);

						// Command timeout
						sqlOptions.CommandTimeout(30);
					});

				// Enable sensitive data logging in development
#if DEBUG
				options.EnableSensitiveDataLogging();
				options.EnableDetailedErrors();
#endif
			});

			// Register Unit of Work
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Register generic repository
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			return services;
		}

		/// <summary>
		/// Ensures the database is created and migrations are applied
		/// </summary>
		/// <param name="serviceProvider">Service provider</param>
		public static void EnsureDatabaseCreated(this IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<IPSDbContext>();

			// Apply pending migrations
			context.Database.Migrate();
		}
	}
}