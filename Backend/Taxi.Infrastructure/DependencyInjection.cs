using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taxi.Application.Abstractions.Api;
using Taxi.Application.Reviews.CalculateReview;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Review;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;
using Taxi.Infrastructure.Data;
using Taxi.Infrastructure.Repositories;

namespace Taxi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var connectionString =
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRideRepository, RideRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IApiService, Taxi.Infrastructure.ApiService.ApiService>();
        services.AddTransient<CalculateReview>();
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }
}
