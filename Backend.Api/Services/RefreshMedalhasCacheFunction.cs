using Backend.Application.Contracts.Services.Caching;
using Backend.Application.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Backend.Api.Services
{
    public class RefreshMedalhasCacheFunction
    {
        private readonly ILogger _logger;
        private readonly IMedalhaRepository _medalhaRepository;
        private readonly ICachingService _cachingService;

        public RefreshMedalhasCacheFunction(
            ILoggerFactory loggerFactory,
            IMedalhaRepository medalhaRepository,
            ICachingService cachingService
        )
        {
            _logger = loggerFactory.CreateLogger<RefreshMedalhasCacheFunction>();
            _medalhaRepository = medalhaRepository;
            _cachingService = cachingService;
        }

        [Function("RefreshMedalhasCacheFunction")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                _cachingService.Connect();

                var medalhas = await _medalhaRepository.GetAsync();

                foreach (var medalha in medalhas)
                {
                    var cacheKey = $"medalhas.{medalha.Pais}";
                    var cache = _cachingService.Read(cacheKey);
                    if (cache is not null)
                    {
                        _cachingService.Delete(cacheKey);
                    }

                    _cachingService.Create(cacheKey, JsonSerializer.Serialize(medalha));
                }
            }
            catch (Exception)
            {
                _cachingService.Disconnect();
                throw;
            }
            
        }
    }
}
