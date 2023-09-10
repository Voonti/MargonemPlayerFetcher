using MargoFetcher.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Infrastructure.Middleware.ErrorHandlingMiddleware
{
    public static class ErrorDetailsExtension
    {
        public static ErrorDetails ToErrorDeatails(this Exception ex, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            return new ErrorDetails() { StatusCode = code, ExceptionMessage = ex.Message, InnerException = Convert.ToString(ex.InnerException) };
        }
    }
}
