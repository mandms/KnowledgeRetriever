using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace DocumentSearch
{
    public class QdrantService(IQdrantClient qdrantClient): IQdrantService
    {
        private const string CollectionName = "documents";

        public async Task<IReadOnlyList<ScoredPoint>> SearchAsync(
            float[] vector,
            int limit = 5)
        {
            return await qdrantClient.SearchAsync(
                collectionName: CollectionName,
                vector: vector,
                limit: (ulong)limit
            );
        }
    }
}
