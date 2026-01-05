namespace DataProcessingWorker
{
    public interface IProcess
    {
        public Task IndexDocumentAsync(string filePath);
    }
}
