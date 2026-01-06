using System.Text.Json.Serialization;

namespace DocumentSearch
{
    public class ChatRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "xiaomi/mimo-v2-flash:free";
        [JsonPropertyName("messages")]
        public Message[] Messages { get; set; }
        [JsonPropertyName("reasoning")]
        public Reasoning Reasoning { get; set; } = new Reasoning { Enabled = true };
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class Reasoning
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }

    public class ChatCompletionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created")]
        public long Created { get; set; }
        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
