using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.Service
{
    public class OrganizerService
    {
        private readonly HttpClient _httpClient;

        public OrganizerService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7140/api/");
        }

        public async Task<List<OrganizerModel>> GetAllOrganizersAsync()
        {
            var response = await _httpClient.GetAsync("organizers");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OrganizerModel>>(jsonResponse);
        }

        public async Task<OrganizerModel> GetOrganizerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"organizers/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrganizerModel>(jsonResponse);
        }

        public async Task CreateOrganizerAsync(OrganizerModel organizerModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(organizerModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("organizers", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrganizerAsync(int id, OrganizerModel organizerModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(organizerModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"organizers/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrganizerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"organizers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
