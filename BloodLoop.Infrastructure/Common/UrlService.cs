using BloodCore;
using BloodCore.AspNet;

namespace BloodLoop.Application.Services
{
    [Injectable]
    public class UrlService : IUrlService
    {
        private readonly WebSettings _webSettings;

        public UrlService(WebSettings webSettings)
        {
            _webSettings = webSettings;
        }

        public string CreateWebUrl(string address, params string[] parameters)
        {
            address = address.TrimStart('/');

            return CreateUrl(_webSettings.WebUrl, address, parameters);
        }

        public string CreateUrl(string url, string address, params string[] parameters)
        {
            url = url.TrimEnd('/');

            return $"{url}/{string.Format(address, parameters)}";
        }
    }
}
