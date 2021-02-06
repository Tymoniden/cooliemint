using System;
using System.Net;

namespace ApplicationStarter.Services.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode, string message) : 
            base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(HttpStatusCode statusCode, string message, Exception innerException) : 
            base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
