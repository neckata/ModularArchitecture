using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ModularArchitecture.Shared.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ModularArchitecture.Shared.Infrastructure.Interceptors
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            List<ValidationFailure> failures = result.Errors.Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                List<string> errorMessages = failures.Select(a => a.ErrorMessage).Distinct().ToList();
                throw new CustomValidationException(errorMessages);
            }

            return result;
        }
    }
}