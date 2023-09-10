using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using MargoFetcher.Infrastructure.Services;
using System.Net.Http.Headers;
using Common;
using MargoFetcher.Infrastructure.ConsolePrinter;

namespace MargoFetcher.Infrastructure.IoC
{
    public static class FetcherService
    {
        public static IServiceCollection AddFetcherService(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IGarmoryApiService, GarmoryApiService>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPrinter, Printer>();
            services.AddTransient<IDispatcherService, DispatcherService>();

            services.AddDbContext<MargoDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);

            services.AddHttpClient(GlobalParameters.ITEM_CLIENT, client =>
            {
                client.BaseAddress = new Uri("https://mec.garmory-cdn.cloud/pl/");
                //client.Timeout = new TimeSpan(10);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient(GlobalParameters.PLAYER_CLIENT, client =>
            {
                client.BaseAddress = new Uri("https://public-api.margonem.pl/info/online/");
                //client.Timeout = new TimeSpan(10);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
    }
}
