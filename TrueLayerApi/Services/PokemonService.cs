using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrueLayerApi.Models;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace TrueLayerApi.Services
{
    public class PokemonService:IPokemonService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PokemonService> _logger;
        private IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly string transaltionKey="translationfor";

        public PokemonService(IConfiguration configuration, ILogger<PokemonService> logger,
            IMemoryCache memoryCache, IMapper mapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<PokemonResponseDto> GetCachedResponse(string testWord)
        {
            var result = new PokemonResponseDto();
            try
            {
                var cacheKey = transaltionKey + testWord;
                result= await GetResponse(cacheKey,testWord,GetUsersSemaphore);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch from ShakeSpeare Api {ex.Message}");
            }
            return result;
        }

        private async Task<PokemonResponseDto> GetResponse(string cacheKey,string testWord, SemaphoreSlim semaphore)
        {
            bool isAvailable = _memoryCache.TryGetValue(cacheKey, out PokemonResponseDto result);
            if (isAvailable) return result;
            try
            {
                await semaphore.WaitAsync();
                isAvailable = _memoryCache.TryGetValue(cacheKey, out result);
                if (isAvailable) return result;
                result = await GetTranslationFromApi(testWord);
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30),
                    Size = 1024
                };
                _memoryCache.Set(cacheKey, result, cacheOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch from Cache {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }    
            return result;
        }

        private async Task<PokemonResponseDto> GetTranslationFromApi(string testWord)
        {
            var result = new PokemonResponseDto();
            var shakespearApi = _configuration["Shakespeare:Api"] + $"?text={testWord}";
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(shakespearApi);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var serializedResponse = JsonConvert.DeserializeObject<ShakespeareResponse>(apiResponse);
                result = _mapper.Map<PokemonResponseDto>(serializedResponse);

                if (serializedResponse.Contents == null)_logger.LogError($"Failed to fetch from ShakeSpeare Api {serializedResponse.Error.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch from ShakeSpeare Api {ex.Message}");
                result.Errormessage = "Not able to server your request. Please contact Admin";
                return result;
            }

            return result;
        }
    }
}
