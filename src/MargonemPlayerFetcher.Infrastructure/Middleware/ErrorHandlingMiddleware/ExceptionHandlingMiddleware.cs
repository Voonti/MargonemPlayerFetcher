using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MargoFetcher.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MargoFetcher.Infrastructure.Middleware.ErrorHandlingMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDeatails(ex.ResponseCode));
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDeatails());
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, ErrorDetails details)
        {
            string error = JsonConvert.SerializeObject(details, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.StatusCode = (int)details.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(error);
        }
    }
}
