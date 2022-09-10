using ISPH.Application.Services;
using ISPH.Application.Services.Advertisements;
using ISPH.Application.Services.Messaging;
using ISPH.Application.Services.Users;
using ISPH.Shared.Interfaces;
using ISPH.Shared.Interfaces.Advertisements;
using ISPH.Shared.Interfaces.Users;

namespace ISPH.Api.Extensions;

public static class ServicesDependencies
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentsService, StudentsService>();
        services.AddScoped<IAdvertisementsService, AdvertisementsService>();
        services.AddScoped<ICompaniesService, CompaniesService>();
        services.AddScoped<IEmployersService, EmployersService>();
        services.AddScoped<IPositionsService, PositionsService>();
        services.AddScoped<ChatService>();
        services.AddScoped<IResumesService, ResumesService>();
        services.AddScoped<IFeaturedAdvertisementsService, FeaturedAdvertisementsService>();
        services.AddScoped<IAdvertisementResponsesService, AdvertisementResponsesService>();
    }
}