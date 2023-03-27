using MargoFetcher.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace MargoFetcher.Domain.Exceptions
{
    public class InvalidPlayerLevelException : DomainException
    {
        public InvalidPlayerLevelException(string errorMessage) : base(HttpStatusCode.Conflict, errorMessage) { }
    }
}
