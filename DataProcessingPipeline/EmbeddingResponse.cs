using System.Text.Json.Serialization;

namespace DataProcessingPipeline
{
    public class EmbeddingResponse
    {
        [JsonPropertyName("created")]
        public long Created { get; set; }
        [JsonPropertyName("object")]
        public string Object {  get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("data")]
        public List<Embeddings> Data {  get; set; }
    }

    public class Embeddings
    {
        [JsonPropertyName("embedding")]
        public required float[] Embedding { get; set; }
    }
}
