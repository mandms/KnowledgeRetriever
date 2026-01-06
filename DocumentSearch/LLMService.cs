using System.Text;
using System.Text.Json;

namespace DocumentSearch
{
    public class LLMService(HttpClient httpClient): ILLMService
    {
        private const string BaseUrl = "https://openrouter.ai/api/v1/chat/completions";

        public async Task<string> GetCompletionAsync(string search)
        {
            var request = new ChatRequest {
                Messages = [new Message { Role = "user", Content = search } ],
                Reasoning = new Reasoning { Enabled = true }
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent).Choices[0].Message.Content;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorContent}");
            }
        }
    }
}
