using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.Service
{
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7140/api/");
        }

        public async Task<List<EventModel>> GetAllEventsAsync()
        {
            var response = await _httpClient.GetAsync("events");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventModel>>(jsonResponse);
        }

        public async Task<EventModel> GetEventByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"events/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventModel>(jsonResponse);
        }

        public async Task CreateEventAsync(EventModel eventModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(eventModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("events", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEventAsync(int id, EventModel eventModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(eventModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"events/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEventAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"events/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
