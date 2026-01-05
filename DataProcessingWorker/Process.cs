using AllMiniLmL6V2Sharp;
using DataProcessingWorker.IngestionService;

namespace DataProcessingWorker
{
    public class Process(IQdrantService qdrantService, ILogger<IProcess> logger): IProcess
    {
        public async Task IndexDocumentAsync(string filePath)
        {
            var text = DocxParser.ParseDocx(filePath);
            var chunks = DocxParser.ChunkText(text);
            List<(string, float[])> vectors = new();

            using var embedder = new AllMiniLmL6V2Embedder();

            foreach (var chunk in chunks)
            {
                logger.Log(LogLevel.Information, String.Join("\n -------------------------------------->", chunks));
                var vector = embedder.GenerateEmbedding(chunk).ToArray();
                vectors.Add((chunk, vector));
            }

            await qdrantService.UpsertDocumentAsync(Guid.NewGuid().ToString(), vectors);

            logger.Log(LogLevel.Information, String.Join("\n", vectors.Select(floatArray =>
            "[" + string.Join(", ", floatArray.Item2) + "]")));

            logger.Log(LogLevel.Information, "Worker stopped");
        }
    }
}
