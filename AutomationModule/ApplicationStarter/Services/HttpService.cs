using ApplicationStarter.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationStarter.Services
{
    public interface IHttpService
    {
        Task<string> SendGETRequest(string uri, Dictionary<string, string> queryParmeter, CancellationToken cancellationToken);
        Task<string> SendPOSTRequest(string uri, Dictionary<string, string> queryParmeter, Dictionary<string, string> body, CancellationToken cancellationToken);
        Task<byte[]> DownloadFile(string uri);
    }

    public class HttpService : IHttpService
    {
        HttpClient _client = new HttpClient();

        public HttpService()
        {
            _client.DefaultRequestHeaders.Add("User-Agent", "Tymoniden");
        }

        public void SetHeaders(Dictionary<string,string> headers)
        {
            
        }

        public async Task<string> SendGETRequest(string uri, Dictionary<string, string> queryParmeter, CancellationToken cancellationToken)
        {
            var queryString = $"{uri}{EncodeQueryParameters(queryParmeter)}";
            var response = await _client.GetAsync(queryString, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new HttpException(response.StatusCode, $"The request {queryString} returned: {(int)response.StatusCode}:{response.StatusCode}");
        }

        public Task<string> SendPOSTRequest(string uri, Dictionary<string, string> queryParmeter, Dictionary<string, string> body, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //var queryString = $"{uri}{EncodeQueryParameters(queryParmeter)}";
            //var content = await _client.PostAsync(queryString);

            //throw new HttpException(response.StatusCode, $"The request {queryString} returned: {(int)response.StatusCode}:{response.StatusCode}");
        }

        public async Task<byte[]> DownloadFile(string uri) => await _client.GetByteArrayAsync(uri);

        string EncodeQueryParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any())
            {
                return string.Empty;
            }

            return $"?{string.Join("&", parameters.Select(parameter => $"{parameter.Key}={HttpUtility.UrlEncode(parameter.Value)}"))}";
        }
    }
}
