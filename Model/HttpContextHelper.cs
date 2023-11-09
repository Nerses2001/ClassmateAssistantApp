using Microsoft.AspNetCore.Http;

namespace Model
{
    public class HttpContextHelper
    {
        public static string? GetClientIpAddress(HttpContext httpContext)
        {
            string ? ip = null;

            var ipAddress = httpContext.Connection.RemoteIpAddress;
            if (ipAddress != null)
                ip = ipAddress.ToString();


            return ip;
        }

        public static string DeserializeFromStream(Stream stream)
        {
            string content = "";

            using (StreamReader reader = new(stream))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}
