using ResidentApi.BusinessLogic.UtilityService.DTOs;
using System.Net;
using System.Text.Json;

namespace ResidentApi.BusinessLogic.UtilityService
{
    public class ExternalService : IExternalService
    {
        private readonly HttpClient _httpClient;
        public ExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ResidentUtilityDto?> GetResidentUtilityBalanceByIdAsync(string residentEmail)
        {
            var response = await _httpClient.GetAsync($"/balances/v2/{residentEmail}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };
                    return JsonSerializer.Deserialize<ResidentUtilityDto>(responseStr, options);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
