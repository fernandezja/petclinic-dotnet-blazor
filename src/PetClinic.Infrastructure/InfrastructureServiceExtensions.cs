using PetClinic.Infrastructure.Data;
using PetClinic.Infrastructure.Repositories;

namespace PetClinic.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = configuration["DatabaseProvider"] ?? "Sqlite";
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=petclinic.db";

        services.AddDbContext<PetClinicDbContext>(options =>
        {
            _ = provider switch
            {
                "SqlServer" => options.UseSqlServer(connectionString),
                _ => options.UseSqlite(connectionString)
            };
        });

        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPetTypeRepository, PetTypeRepository>();
        services.AddScoped<IVetRepository, VetRepository>();
        services.AddMemoryCache();
        services.Configure<CacheOptions>(options =>
        {
            var section = configuration.GetSection("Cache");
            if (double.TryParse(section["VetsCacheDurationMinutes"], out var minutes))
                options.VetsCacheDurationMinutes = minutes;
        });

        return services;
    }
}
