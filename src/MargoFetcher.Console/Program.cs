using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MargoFetcher.Infrastructure.IoC;
using MargoFetcher.Application.Jobs.Commands;
using Microsoft.Extensions.Configuration;
using MargoFetcher.Presentation;

var services = new ServiceCollection(); ;
var configBuilder = new ConfigurationBuilder();

configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = configBuilder.Build();

var connectionString = configuration.GetConnectionString("MargoDB");

services.AddFetcherService(connectionString);
services.AddMediatR(typeof(SyncPlayerCommandHandler).Assembly);

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();

var presentation = new Presentation(mediator);
await presentation.Run();