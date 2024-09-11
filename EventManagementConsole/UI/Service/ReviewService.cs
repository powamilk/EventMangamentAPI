using EventManagementConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagementConsole.UI.Service
{
    public class ReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7140/api/");
        }

        public async Task<List<ReviewModel>> GetAllReviewsAsync()
        {
            var response = await _httpClient.GetAsync("reviews");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ReviewModel>>(jsonResponse);
        }

        public async Task<ReviewModel> GetReviewByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"reviews/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ReviewModel>(jsonResponse);
        }

        public async Task CreateReviewAsync(ReviewModel reviewModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(reviewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("reviews", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateReviewAsync(int id, ReviewModel reviewModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(reviewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"reviews/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"reviews/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
