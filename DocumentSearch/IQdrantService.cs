using Qdrant.Client.Grpc;

namespace DocumentSearch
{
    public interface IQdrantService
    {
        public Task<IReadOnlyList<ScoredPoint>> SearchAsync(float[] vector, int limit = 5);
    }
}
