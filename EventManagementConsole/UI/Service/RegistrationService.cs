using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.Service
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public RegistrationService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7140/api/");
        }

        public async Task<List<RegistrationModel>> GetAllRegistrationsAsync()
        {
            var response = await _httpClient.GetAsync("registrations");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<RegistrationModel>>(jsonResponse);
        }

        public async Task<RegistrationModel> GetRegistrationByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"registrations/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RegistrationModel>(jsonResponse);
        }

        public async Task CreateRegistrationAsync(RegistrationModel registrationModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(registrationModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("registrations", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateRegistrationAsync(int id, RegistrationModel registrationModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(registrationModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"registrations/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"registrations/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
