using System.Net;
using System.Text.Json;
using Backend.Application.Contracts;
using Backend.Application.Contracts.Services.Caching;
using Backend.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Api.Services
{
    public class GetMedalhasFunction
    {
        private readonly ILogger _logger;
        private readonly IMedalhaRepository _medalhaRepository;
        private readonly ICachingService _cachingService;

        public GetMedalhasFunction(
            ILoggerFactory loggerFactory,
            IMedalhaRepository medalhaRepository,
            ICachingService cachingService
        )
        {
            _logger = loggerFactory.CreateLogger<GetMedalhasFunction>();
            _medalhaRepository = medalhaRepository;
            _cachingService = cachingService;
        }

        [Function("GetMedalhasFunction")]
        public async Task<IEnumerable<MedalhaEntity>> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "medalhas")] HttpRequestData req)
        {
            var listaMedalhas = new List<MedalhaEntity>();
            foreach (var key in _cachingService.Scan("medalhas"))
            {
                Console.WriteLine(key);
                var medalha = JsonSerializer.Deserialize<MedalhaEntity>(_cachingService.Read(key));
                listaMedalhas.Add(medalha);
            }

            return listaMedalhas;
        }
    }
}
