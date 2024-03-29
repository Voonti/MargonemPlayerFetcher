﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MargoFetcher.Infrastructure.IoC;
using MargoFetcher.Application.Jobs.Commands;
using Microsoft.Extensions.Configuration;
using MargoFetcher.Presentation;
using MargoFetcher.Domain.Interfaces;

var services = new ServiceCollection(); ;
var configBuilder = new ConfigurationBuilder();

configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = configBuilder.Build();

var connectionString = configuration.GetConnectionString("MargoDB");


services.AddFetcherService(connectionString);
services.AddMediatR(typeof(SyncPlayerCommandHandler).Assembly);

var serviceProvider = services.BuildServiceProvider();

var dispatcherService = serviceProvider.GetRequiredService<IDispatcherService>();

var presentation = new Presentation(dispatcherService);
await presentation.Run();