using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.SharedKernel.Middlewares
{
    /// <summary>
    /// Request and Response Logging Middleware.
    /// Document: https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequestAsync(context);
            await LogResponseAsync(context);
        }

        private async Task LogResponseAsync(HttpContext context)
        {
            await using var originalBodyStream = context.Response.Body;
            try
            {
                await using var responseBody = _recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(responseBody).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

                _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                        $"Schema:{context.Request.Scheme} " +
                                        $"Host: {context.Request.Host} " +
                                        $"Path: {context.Request.Path} " +
                                        $"QueryString: {context.Request.QueryString} " +
                                        $"Response Body: {text}");
            }
            finally
            {
                //Always be executed
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task LogRequestAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamRequest(requestStream)}");

            context.Request.Body.Position = 0;
        }

        private static string ReadStreamRequest(Stream requestStream)
        {
            string requestBody;
            requestStream.Position = 0;
            using (StreamReader streamReader = new StreamReader(requestStream))
            {
                requestBody = streamReader.ReadToEnd();
            }

            return requestBody;
        }
    }
}
