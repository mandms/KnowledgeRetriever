using AllMiniLmL6V2Sharp;

namespace DocumentSearch
{
    public class Search(IQdrantService qdrantService, ILLMService llmService): ISearch
    {
        public async Task<string> Process(string search)
        {
            using var embedder = new AllMiniLmL6V2Embedder();
            var vector = embedder.GenerateEmbedding(search).ToArray();

            var responses = await qdrantService.SearchAsync(vector);

            var responseString = String.Join("\n", responses.Select(response => {
                if (response.Payload.TryGetValue("text", out var textValue))
                {
                    return textValue.StringValue;
                }
                return string.Empty;
            }));

            string prompt = $"Информация из базы знаний:\n {responseString}\nВопрос: {search}\nОтвет (на основе информации выше):";

            var deepSeekResponse = await llmService.GetCompletionAsync(prompt);

            return deepSeekResponse;
        }
    }
}
