namespace DataProcessingWorker
{
    public interface IQdrantService
    {
        public Task TryCreateCollectionAsync(int vectorSize = 384);

        public Task UpsertDocumentAsync(string documentId,
            List<(string Text, float[] Vector)> chunks);
    }
}
