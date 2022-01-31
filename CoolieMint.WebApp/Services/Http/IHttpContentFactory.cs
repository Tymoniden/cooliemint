using System.Net.Http;

namespace CoolieMint.WebApp.Services.Http
{
    public interface IHttpContentFactory
    {
        HttpContent CreateJsonStringContent(string content);
    }
}