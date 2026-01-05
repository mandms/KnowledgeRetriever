using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace DataProcessingWorker
{
    public class QdrantService(IQdrantClient qdrantClient, ILogger<IQdrantService> logger) : IQdrantService
    {
        private const string CollectionName = "documents";

        public async Task TryCreateCollectionAsync(int vectorSize = 384)
        {
            try
            {
                var collections = await qdrantClient.ListCollectionsAsync();
                var collectionExists = collections.Any(c => c == CollectionName);

                if (collectionExists)
                {
                    logger.Log(LogLevel.Information, $"Коллекция '{CollectionName}' уже существует.");
                    return;
                }

                await qdrantClient.CreateCollectionAsync(
                    collectionName: CollectionName,
                    vectorsConfig: new VectorParams
                    {
                        Size = (ulong)vectorSize,
                        Distance = Distance.Cosine
                    }
                );
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"Ошибка при создании коллекции: {ex.Message}");
                throw;
            }
        }

        public async Task UpsertDocumentAsync(
            string documentId,
            List<(string Text, float[] Vector)> chunks)
        {
            if (chunks.Count == 0)
            {
                Console.WriteLine("LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOL");
                return;
            }
            var points = new List<PointStruct>();

            for (int i = 0; i < chunks.Count; i++)
            {
                var point = new PointStruct
                {
                    Id = (ulong)($"{documentId}_{i}".GetHashCode()),
                    Vectors = chunks[i].Vector,
                    Payload = {
                        ["document_id"] = documentId,
                        ["chunk_index"] = i,
                        ["text"] = chunks[i].Text,
                        ["chunk_count"] = chunks.Count
                    }
                };

                points.Add(point);
            }

            await qdrantClient.UpsertAsync(CollectionName, points);
        }
    }
}
