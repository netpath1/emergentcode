using System;

namespace Software.Server
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException() { }
        public HttpResponseException(string msg) : base(msg) { }

        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
