using System.Net.Http.Json;

namespace DataProcessingPipeline
{
    public class EmbeddingGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;

        public EmbeddingGenerator(
            string baseUrl = "http://localhost:8080/",
            string model = "phi-2.Q4_0.gguf")
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _model = model;
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var request = new
            {
                input = text,
                model = _model
            };

            var response = await _httpClient.PostAsJsonAsync("v1/embeddings", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();
            return result.Data[0].Embedding;
        }
    }
}
