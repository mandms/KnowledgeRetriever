using Grpc.Net.Client;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace DataProcessingPipeline
{

    public class QdrantService
    {
        private readonly QdrantClient _client;
        private const string CollectionName = "documents";

        public QdrantService(string host = "localhost", int port = 6333)
        {
            _client = new QdrantClient("localhost");
        }

        public async Task CreateCollectionAsync(int vectorSize = 384)
        {
            await _client.CreateCollectionAsync(
                collectionName: CollectionName,
                vectorsConfig: new VectorParams
                {
                    Size = (ulong)vectorSize,
                    Distance = Distance.Cosine
                }
            );
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
                    Payload =
                {
                    ["document_id"] = documentId,
                    ["chunk_index"] = i,
                    ["text"] = chunks[i].Text,
                    ["chunk_count"] = chunks.Count
                }
                };

                points.Add(point);
            }

            await _client.UpsertAsync(CollectionName, points);
        }
    }
}
