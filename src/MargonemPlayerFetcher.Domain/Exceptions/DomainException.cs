using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Exceptions
{
    public abstract class DomainException : ResponseException
    {
        public DomainException(HttpStatusCode responseCode, string errorMessage) : base(responseCode, errorMessage) { }
    }
}
