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

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFetcherService();

builder.Services.AddDbContext<MargoDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("MargoDB"));
});
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
