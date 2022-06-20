using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModularArchitecture.Shared.Core.Exceptions;
using ModularArchitecture.Shared.Core.Interfaces.Serialization;
using ModularArchitecture.Shared.Core.Serialization;
using ModularArchitecture.Shared.Core.Settings;
using ModularArchitecture.Shared.Core.Wrapper;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Infrastructure.Middlewares
{
    internal class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly SerializationSettings _serializationSettings;
        private readonly IJsonSerializer _jsonSerializer;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IOptions<SerializationSettings> serializationSettings,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _serializationSettings = serializationSettings.Value;
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                ErrorResult<string> responseModel = await ErrorResult<string>.ReturnErrorAsync(exception.Message);
                responseModel.Source = exception.Source;
                responseModel.Exception = exception.Message;
                _logger.LogError(exception.Message);
                switch (exception)
                {
                    case CustomException e:
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = string.Empty;
                if (_serializationSettings.UseNewtonsoftJson)
                {
                    result = _jsonSerializer.Serialize(responseModel, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    });
                }
                else if (_serializationSettings.UseSystemTextJson)
                {
                    result = _jsonSerializer.Serialize(responseModel, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerOptions = { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    });
                }

                await response.WriteAsync(result);
            }
        }
    }
}