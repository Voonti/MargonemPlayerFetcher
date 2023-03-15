using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Exceptions
{
    public class ResponseException : Exception
    {
        public HttpStatusCode ResponseCode { get; set; }
        public ResponseException(HttpStatusCode responseCode, string errorMessage) : base(errorMessage)
        {
            ResponseCode = responseCode;
        }
    }
}
