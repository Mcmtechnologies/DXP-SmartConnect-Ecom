using DXP.SmartConnect.Ecom.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DXP.SmartConnect.Ecom.SharedKernel.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Sets an authorization header with a bearer token.
        /// </summary>
        /// <param name="request">The HttpRequestMessage</param>
        /// <param name="token">The Token</param>
        public static void SetBearerToken(this HttpRequestMessage request, string token)
        {
            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// Sets a header content type.
        /// </summary>
        /// <param name="request">The HttpRequestMessage</param>
        /// <param name="header">The list of header</param>
        public static void SetContentType(this HttpRequestMessage request, Dictionary<string, string> header)
        {
            if (header != null && request.Content != null && header.TryGetValue(WebApiContent.HeaderContentType, out string headerValue) && MediaTypeHeaderValue.TryParse(headerValue, out MediaTypeHeaderValue mediaTypeValue))
            {
                request.Content.Headers.ContentType = mediaTypeValue;
            }
        }

        /// <summary>
        /// Sets a header of request.
        /// </summary>
        /// <param name="request">The HttpRequestMessage</param>
        /// <param name="header">The list of header</param>
        public static void SetHeader(this HttpRequestMessage request, Dictionary<string, string> header)
        {
            if (header != null)
            {
                foreach (var h in header)
                {
                    if (!string.IsNullOrEmpty(h.Value) && !h.Key.Equals(WebApiContent.HeaderContentType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }
            }
        }
    }
}
