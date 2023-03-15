using System.Net;

namespace UnitessTestApp.Api.Core.Exceptions
{
    public class UnitessException : Exception
    {
        public HttpStatusCode Status { get; set; }

        public UnitessException(HttpStatusCode code, string msg) : base(msg)
        {
            Status = code;
        }
    }
}
