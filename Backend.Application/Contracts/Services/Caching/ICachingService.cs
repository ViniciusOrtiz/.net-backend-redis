
namespace Backend.Application.Contracts.Services.Caching
{
    public interface ICachingService
    {
        void Connect();
        void Disconnect();
        void Create(string key, string value, TimeSpan? expiry = null);
        string Read(string key);
        void Update(string key, string value, TimeSpan? expiry = null);
        void Delete(string key);
        IEnumerable<string> Scan(string prefix);
    }
}
