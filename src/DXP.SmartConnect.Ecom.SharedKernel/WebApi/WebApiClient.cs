using DXP.SmartConnect.Ecom.SharedKernel.Extensions;
using DXP.SmartConnect.Ecom.SharedKernel.ValueObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.SharedKernel.WebApi
{
    public class WebApiClient
    {
        private readonly ILogger<WebApiClient> _logger;
        private readonly HttpClient _httpClient;

        public WebApiClient(ILogger<WebApiClient> logger, HttpClient client)
        {
            _httpClient = client;
            _logger = logger;
        }

        /// <summary>
        /// Send an GET HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="path">The path of Uri the request is sent to</param>
        /// <param name="accessToken">The access token of request</param>
        /// <param name="header">The header of request</param>
        /// <param name="httpStatusCodesSuccessfully">The status codes indicates the request is successful</param>
        /// <returns>Task of response message</returns>
        public async Task<T> GetAsync<T>(string path,
                                         string accessToken = null,
                                         Dictionary<string, string> header = null,
                                         HttpStatusCode[] httpStatusCodesSuccessfully = null) where T : new()
        {
            httpStatusCodesSuccessfully = httpStatusCodesSuccessfully ?? HttpStatusCodes.DefaultSuccessfully;
            T value = default(T);

            var watch = new Stopwatch();
            watch.Start();

            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                // Add access token.
                request.SetBearerToken(accessToken);

                // Add header.
                request.SetHeader(header);

                response = await _httpClient.SendAsync(request);
            }

            watch.Stop();
            _logger.LogInformation($"Response time of {nameof(GetAsync)} ({path}): {watch.ElapsedMilliseconds} ms");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!httpStatusCodesSuccessfully.Contains(response.StatusCode))
            {
                HandleHttpError(response.StatusCode, responseContent);
            }

            try
            {
                value = JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Reason when DeserializeObject: {responseContent} is {ex.Message}");
            }

            return value;
        }

        /// <summary>
        /// Send an POST HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="path">The path of Uri the request is sent to</param>
        /// <param name="method">Method used by the HTTP request message</param>
        /// <param name="accessToken">The access token of request</param>
        /// <param name="content">HTTP entity body and content headers</param>
        /// <param name="header">The header of request</param>
        /// <param name="httpStatusCodesSuccessfully">The status codes indicates the request is successful</param>
        /// <returns>Task of response message</returns>
        public async Task<T> PostAsync<T>(string path,
                                          HttpMethod method,
                                          HttpContent content,
                                          string accessToken = null,
                                          Dictionary<string, string> header = null,
                                          HttpStatusCode[] httpStatusCodesSuccessfully = null) where T : new()
        {
            httpStatusCodesSuccessfully ??= HttpStatusCodes.DefaultSuccessfully;
            T value = default(T);

            var watch = new Stopwatch();
            watch.Start();

            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(method, path))
            {
                // Add access token.
                request.SetBearerToken(accessToken);

                // Add body.
                request.Content = content;

                // Add header content type.
                request.SetContentType(header);

                // Add header.
                request.SetHeader(header);

                response = await _httpClient.SendAsync(request);
            }

            watch.Stop();
            _logger.LogInformation($"Response time of {nameof(PostAsync)} ({path}): {watch.ElapsedMilliseconds} ms");

            // Return response is true (or false) if T type is bool
            if (typeof(T) == typeof(bool))
            {
                if (httpStatusCodesSuccessfully.Contains(response.StatusCode))
                    value = (T)Convert.ChangeType(true, typeof(T));
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!httpStatusCodesSuccessfully.Contains(response.StatusCode))
                {
                    HandleHttpError(response.StatusCode, responseContent);
                }

                value = JsonConvert.DeserializeObject<T>(responseContent);
            }

            return value;
        }

        private static void HandleHttpError(HttpStatusCode statusCode, string content)
        {
            throw new HttpResponseException(statusCode, content);
        }
    }
}
