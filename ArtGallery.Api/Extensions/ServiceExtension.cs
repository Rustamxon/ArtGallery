using ArtGallery.DAL.IRepositories;
using ArtGallery.DAL.Repositories;


namespace ArtGallery.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
