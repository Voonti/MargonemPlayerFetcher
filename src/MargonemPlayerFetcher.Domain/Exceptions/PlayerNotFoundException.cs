using MargonemPlayerFetcher.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Exceptions
{
    public class PlayerNotFoundException : DomainException
    {
        public PlayerNotFoundException(string errorMessage) : base(HttpStatusCode.NotFound, errorMessage) { }
    }
}
