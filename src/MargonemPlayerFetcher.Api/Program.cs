using MargonemPlayerFetcher.Domain.Interfaces;
using MargonemPlayerFetcher.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using MargonemPlayerFetcher.Application.IoC;
using MargonemPlayerFetcher.Infrastructure.DbContexts;
using MargonemPlayerFetcher.Infrastructure.Middleware.ErrorHandlingMiddleware;
using FluentValidation.AspNetCore;
using MargonemPlayerFetcher.Domain.Validators;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meetings.Api", Version = "v1" });
});

builder.Services.AddFetcherService();

builder.Services.AddDbContext<MargoDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("MargoDB"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetings.Api v1"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
