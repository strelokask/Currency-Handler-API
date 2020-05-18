using Currency_Handler_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Currency_Handler_API.Services
{
    public class CurrencyService
    {

        private HttpClient httpClient;
        private const string requestUri = "https://www.cbr-xml-daily.ru/daily_json.js";
        public CurrencyService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
        /// <summary>
        /// Send request to specific uri to get the data
        /// Returns the data of valuta 
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throw exception if response is not succeeded
        /// </exception> 
        /// <returns> Daily valutas </returns>
        public async Task<IEnumerable<Valuta>> GetDataAsync()
        {
            var response = await httpClient.GetAsync(requestUri);
            
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dailyCurrency = await JsonSerializer.DeserializeAsync<DailyCurrency>(responseStream);

                var currencies = dailyCurrency.Valute
                    .GetType()
                    .GetProperties()
                    .Select(prop =>
                        dailyCurrency.Valute
                        .GetType()
                        .GetProperty(prop.Name)
                        .GetValue(dailyCurrency.Valute, null) as Valuta
                    );

                return currencies;
            }

            throw new ArgumentNullException(response.ToString());
        }
    }
}
