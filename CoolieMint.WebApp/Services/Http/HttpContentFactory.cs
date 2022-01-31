using System.Net.Http;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CoolieMint.WebApp.Services.Http
{
    public class HttpContentFactory : IHttpContentFactory
    {
        public HttpContent CreateJsonStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, Application.Json);
        }
    }
}
