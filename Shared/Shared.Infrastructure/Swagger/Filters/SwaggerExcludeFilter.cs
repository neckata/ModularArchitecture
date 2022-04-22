﻿using System;
using System.Linq;
using System.Reflection;
using Gamification.Shared.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gamification.Shared.Infrastructure.Swagger.Filters
{
    public class SwaggerExcludeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.MethodInfo == null)
            {
                return;
            }

            var parameters = context.MethodInfo.GetParameters();
            var properties = parameters.SelectMany(x => x.ParameterType.GetProperties());
            var propertiesToRemove = properties
                .Where(p => p.GetCustomAttribute<SwaggerExcludeAttribute>() != null && p.GetCustomAttribute<FromQueryAttribute>() != null)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

            foreach (var parameter in operation.Parameters.ToList())
            {
                if (propertiesToRemove.Contains(parameter.Name))
                {
                    operation.Parameters.Remove(parameter);
                }
            }
        }
    }
}