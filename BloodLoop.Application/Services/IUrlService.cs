namespace BloodLoop.Application.Services
{
    public interface IUrlService
    {
        string CreateWebUrl(string address, params string[] parameters);
        string CreateUrl(string url, string address, params string[] parameters);
    }
}
