using MargoFetcher.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Exceptions
{
    public class ItemNotFoundException : DomainException
    {
        public ItemNotFoundException(string errorMessage) : base(HttpStatusCode.NotFound, errorMessage) { }
    }
}
