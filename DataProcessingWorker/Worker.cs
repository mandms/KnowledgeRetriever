namespace DataProcessingWorker
{
    public class Worker(ILogger<Worker> logger, IQdrantService qdrantService, IProcess process) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.Log(LogLevel.Information, "Worker started");

            await qdrantService.TryCreateCollectionAsync();

            string currentDirectory = Directory.GetCurrentDirectory();
            string docsFilePath = Directory.GetParent(currentDirectory)?.FullName + "/docs";

            string[] docxFiles = Directory.GetFiles(docsFilePath, "*.docx");


            foreach (string filePath in docxFiles)
            {
                await process.IndexDocumentAsync(filePath);
            }
        }
    }
}
