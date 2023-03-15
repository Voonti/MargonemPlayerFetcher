using MargonemPlayerFetcher.Domain.Interfaces;
using MargonemPlayerFetcher.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using MargonemPlayerFetcher.Domain.Validators;
using FluentValidation.AspNetCore;

namespace MargonemPlayerFetcher.Application.IoC
{
    public static class FetcherService
    {
        public static IServiceCollection AddFetcherService(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ItemValidator>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
