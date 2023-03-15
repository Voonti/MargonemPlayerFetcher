using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Exceptions
{
    public class ErrorDetails// : Exception
    {
        public string ExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
