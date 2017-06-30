using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TransactMe.Services
{
    public class CurrenciesAPIService
    {
        //public static double OfficialRate;

        public double GetOfficialRate(string currency)
        {
            using (var httpClient = new HttpClient {BaseAddress = new Uri("https://api.openapi.ro/")})
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key",
                    "nzjYgxzZcvkBdkuPAFsF71LiTHqvPH9NqmmUYkdjdutwVGv8Rg");

                var requestAddress = $"api/exchange/{currency}?date={DateTime.Today:yyyy-MM-dd}";

                var jsonResult = httpClient.GetAsync(requestAddress)
                    .Result.Content.ReadAsStringAsync().Result;

                return double.Parse(JToken.Parse(jsonResult)["rate"].ToString());
            }
        }

        public double GetOfficialRate()
        {
            using (var httpClient = new HttpClient {BaseAddress = new Uri("https://api.openapi.ro/")})
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key",
                    "nzjYgxzZcvkBdkuPAFsF71LiTHqvPH9NqmmUYkdjdutwVGv8Rg");

                var requestAddress = $"api/exchange/EUR?date={DateTime.Today:yyyy-MM-dd}";

                var jsonResult = httpClient.GetAsync(requestAddress)
                    .Result.Content.ReadAsStringAsync().Result;

                return double.Parse(JToken.Parse(jsonResult)["rate"].ToString());
            }
        }
    }
}