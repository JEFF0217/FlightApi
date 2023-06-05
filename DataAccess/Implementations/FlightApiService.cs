using DataAccess.DTOs;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class FlightApiService : IFlightApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://recruiting-api.newshore.es/api/flights/2";

        public FlightApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<FlightApiDTO>> GetFlightData()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<FlightApiDTO>>(content);
                    return data;
                }

                throw new Exception("Internal Server Error");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error connecting to the flight API. Please try again later.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Error deserializing flight data. Please contact support.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred. Please contact support.", ex);
            }
        }
    }
}
