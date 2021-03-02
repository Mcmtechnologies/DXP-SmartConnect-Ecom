using DXP.SmartConnect.Ecom.SharedKernel;
using System.Net;

namespace DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients
{
    public class HttpStatusSuccessCodes : ValueObject
    {
        /// <summary>
        /// List HttpStatus Codes get shopping cart accepted.
        /// </summary>
        public static HttpStatusCode[] GetCart { get; } = {
           HttpStatusCode.OK, // 200
           HttpStatusCode.NoContent, // 204
           HttpStatusCode.NotFound, // 404
           HttpStatusCode.NotModified // 304
        };

        /// <summary>
        /// List HttpStatus Codes accepted when shopping cart change.
        /// </summary>
        public static HttpStatusCode[] CartChange { get; } = {
           HttpStatusCode.Accepted // 202
        };

        /// <summary>
        /// List HttpStatus Codes for get checkout info accepted.
        /// </summary>
        public static HttpStatusCode[] GetCheckout { get; } = {
            HttpStatusCode.OK, // 200
            HttpStatusCode.NoContent, // 204
            HttpStatusCode.NotFound // 404
        };

        /// <summary>
        /// List HttpStatus Codes accepted when execute change command for checkout.
        /// </summary>
        public static HttpStatusCode[] CheckoutChange { get; } = {
            HttpStatusCode.OK, // 200
            HttpStatusCode.Accepted, // 202
            HttpStatusCode.NoContent, // 204
        };

        /// <summary>
        /// List HttpStatus Codes accepted when execute delete command for checkout.
        /// </summary>
        public static HttpStatusCode[] CheckoutDelete { get; } = {
            HttpStatusCode.OK, // 200
            HttpStatusCode.NoContent, // 204
        };

        /// <summary>
        /// List HttpStatus Codes accepted when order change.
        /// </summary>
        public static HttpStatusCode[] OrderChange { get; } = {
           HttpStatusCode.Accepted // 202
        };

        /// <summary>
        /// List HttpStatus Codes get order accepted.
        /// </summary>
        public static HttpStatusCode[] GetOrder { get; } = {
           HttpStatusCode.OK, // 200
           HttpStatusCode.NoContent, // 204
           HttpStatusCode.NotFound, // 404
           HttpStatusCode.NotModified // 304
        };
    }
}
