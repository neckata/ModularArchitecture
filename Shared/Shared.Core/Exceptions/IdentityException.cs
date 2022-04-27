using System.Collections.Generic;
using System.Net;

namespace Gamification.Shared.Core.Exceptions
{
    public class IdentityException : CustomException
    {
        public IdentityException(string message, List<string> errors = default, HttpStatusCode statusCode = default)
            : base(message, errors, statusCode)
        {
        }
    }
}
