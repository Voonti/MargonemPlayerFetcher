using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using MargoFetcher.Infrastructure.DbContexts;
using MargoFetcher.Infrastructure.Middleware.ErrorHandlingMiddleware;
using FluentValidation.AspNetCore;
using MargoFetcher.Domain.Validators;
using Microsoft.OpenApi.Models;
using MargoFetcher.Infrastructure.IoC;
using Hangfire;
using MargoFetcher.Application.Items.Commands;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meetings.Api", Version = "v1" });
});
var connectionString = builder.Configuration.GetConnectionString("MargoDB");
builder.Services.AddFetcherService(connectionString);
builder.Services.AddJobsService(connectionString);
builder.Services.AddMediatR(typeof(InsertItemsCommandHandler).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetings.Api v1"));
    app.UseHangfireDashboard();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
