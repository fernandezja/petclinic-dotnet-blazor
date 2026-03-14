using PetClinic.Application.Owners;
using PetClinic.Application.Vets;

namespace PetClinic.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IVetService, VetService>();

        services.AddValidatorsFromAssemblyContaining<OwnerService>();

        return services;
    }
}
