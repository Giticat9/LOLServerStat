using LeagueOfLegendsServerStatistics.Application.Riot.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LeagueOfLegendsServerStatistics.Application.Riot.Api
{
    public class SummonerV4: ISummonerV4
    {
        private readonly IConfiguration _configuration;
        private readonly string _riotApiKey;
        private readonly string _riotApiHost;

        public SummonerV4(IConfiguration configuration)
        {
            _configuration = configuration;

            if (_configuration != null)
            {
                _riotApiKey = _configuration.GetValue<string>("Riot:Dev:ApiKey") ?? "";
                _riotApiHost = _configuration.GetValue<string>("Riot:Deb:Host") ?? "";
            }
        }

        public async Task<SummonerModal?> GetSummonerByName(string summonerName)
        {
            try
            {
                HttpClient httpClient = new()
                {
                    BaseAddress = new Uri(_riotApiHost),
                };

                httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _riotApiKey);

                using var request = new HttpRequestMessage(HttpMethod.Get, $"lol/summoner/v4/summoners/by-name/{summonerName}");

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var summonerModel = JsonConvert.DeserializeObject<SummonerModal>(content);

                    return summonerModel;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка получения призывателя по имени", ex);
            }
        }
    }
}
