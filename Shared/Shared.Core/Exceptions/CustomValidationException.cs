﻿using System.Collections.Generic;
using System.Net;

namespace ModularArchitecture.Shared.Core.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(List<string> errors)
            : base("One or more validation failures have occurred.", errors, HttpStatusCode.BadRequest)
        {
        }
    }
}