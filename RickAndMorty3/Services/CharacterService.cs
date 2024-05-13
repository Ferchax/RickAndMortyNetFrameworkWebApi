using Newtonsoft.Json;
using RickAndMorty3.Dtos;
using RickAndMorty3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace RickAndMorty3.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TimeSpan _cacheDuration;

        public CharacterService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _cacheDuration = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings["CacheDurationMinutes"]));
        }

        public async Task<ResponseCharacterDto> GetCharactersAsync()
        {
            string cacheKey = "CharacterCache";
            var cachedResult = HttpRuntime.Cache[cacheKey] as ResponseCharacterDto;

            if (cachedResult != null) return cachedResult;

            var client = _httpClientFactory.CreateClient("RickAndMortyAPI");
            var response = await client.GetAsync("character");

            if (response.IsSuccessStatusCode)
            {                
                var jsonContent = await response.Content.ReadAsStringAsync();
                var responseCharacterDto = JsonConvert.DeserializeObject<ResponseCharacterDto>(jsonContent);

                HttpRuntime.Cache.Insert(cacheKey, responseCharacterDto, null, Cache.NoAbsoluteExpiration, _cacheDuration);

                return responseCharacterDto;
            }

            return null;
        }
    }
}