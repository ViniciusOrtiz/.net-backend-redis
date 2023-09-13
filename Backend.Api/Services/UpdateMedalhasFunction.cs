using System.Text.Json;
using Backend.Application.Contracts;
using Backend.Application.Contracts.Services.Caching;
using Backend.Application.Exceptions;
using Backend.Application.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Api.Services
{
    public class UpdateMedalhasFunction
    {
        private readonly ILogger _logger;
        private readonly IMedalhaRepository _medalhaRepository;
        private readonly ICachingService _cachingService;

        public UpdateMedalhasFunction(
            ILoggerFactory loggerFactory,
            IMedalhaRepository medalhaRepository,
            ICachingService cachingService
        )
        {
            _logger = loggerFactory.CreateLogger<UpdateMedalhasFunction>();
            _medalhaRepository = medalhaRepository;
            _cachingService = cachingService;
        }

        [Function("UpdateMedalhasFunction")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "medalha")] HttpRequestData req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var payload = JsonSerializer.Deserialize<MedalhaDto>(body);

            var medalha = await _medalhaRepository.GetByPais(payload.Pais);

            if(medalha is null)
            {
                throw new NotFoundException("Medalha não encontrada");
            }

            medalha.Ouro = (payload.Ouro > 0) ? payload.Ouro : medalha.Ouro;
            medalha.Prata = (payload.Prata > 0) ? payload.Prata : medalha.Prata;
            medalha.Bronze = (payload.Bronze > 0) ? payload.Bronze : medalha.Bronze;
            medalha.Total = medalha.Ouro + medalha.Prata + medalha.Bronze;

            await _medalhaRepository.UpdateAsync(medalha);

            try
            {
                _cachingService.Connect();

                var cacheKey = $"medalhas.{medalha.Pais}";
                var medalhaSerializada = JsonSerializer.Serialize(medalha);

                var cache = _cachingService.Read(cacheKey);
                if (cache is null)
                {
                    _cachingService.Create(cacheKey, medalhaSerializada);
                    return;
                }

                _cachingService.Update(cacheKey, medalhaSerializada);
            }
            catch (Exception)
            {
                _cachingService.Disconnect();
                throw;
            }
            
        }
    }
}
