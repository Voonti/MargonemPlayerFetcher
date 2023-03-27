using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using MargoFetcher.Domain.Validators;
using FluentValidation.AspNetCore;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MargoFetcher.Infrastructure.IoC
{
    public static class FetcherService
    {
        public static IServiceCollection AddFetcherService(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ItemValidator>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContext<MargoDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
