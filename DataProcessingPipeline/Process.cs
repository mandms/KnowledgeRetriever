using AllMiniLmL6V2Sharp;

namespace DataProcessingPipeline
{
    public class Process
    {
        private readonly QdrantService _qdrantService; 
        public Process()
        {
            _qdrantService = new QdrantService();
            Task.WhenAll(_qdrantService.CreateCollectionAsync());
        }
        public async Task<string> IndexDocumentAsync(string filePath)
        {
            var text = DocxParser.ParseDocx(filePath);
            var chunks = DocxParser.ChunkText(text);
            List<(string, float[])> vectors = new();

            var embeddingsGenerator = new EmbeddingGenerator();
            using var embedder = new AllMiniLmL6V2Embedder(truncate: true);
       
            foreach (var chunk in chunks)
            {
                Console.WriteLine(String.Join("\n -------------------------------------->", chunks));
                var vector = embedder.GenerateEmbedding(chunk).ToArray();
                vectors.Add((chunk, vector));
            }

            await _qdrantService.UpsertDocumentAsync(Guid.NewGuid().ToString(), vectors);

            
            return String.Join("\n", vectors.Select(floatArray =>
            "[" + string.Join(", ", floatArray.Item2) + "]"));
        }
    }
}
