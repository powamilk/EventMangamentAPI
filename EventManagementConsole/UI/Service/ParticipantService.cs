using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.Service
{
    public class ParticipantService
    {
        private readonly HttpClient _httpClient;

        public ParticipantService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7140/api/");
        }

        public async Task<List<ParticipantModel>> GetAllParticipantsAsync()
        {
            var response = await _httpClient.GetAsync("participants");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ParticipantModel>>(jsonResponse);
        }

        public async Task<ParticipantModel> GetParticipantByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"participants/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ParticipantModel>(jsonResponse);
        }

        public async Task CreateParticipantAsync(ParticipantModel participantModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(participantModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("participants", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateParticipantAsync(int id, ParticipantModel participantModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(participantModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"participants/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteParticipantAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"participants/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
