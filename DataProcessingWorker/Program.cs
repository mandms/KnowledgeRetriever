namespace DataProcessingWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddQdrant();

            builder.Services.AddTransient<IQdrantService, QdrantService>();
            builder.Services.AddTransient<IProcess, Process>();

            var host = builder.Build();
            host.Run();
        }
    }
}