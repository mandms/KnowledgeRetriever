using Qdrant.Client;

namespace DataProcessingWorker
{
    public static class QdrantConnection
    {
        public static IServiceCollection AddQdrant(this IServiceCollection services)
        {
            services.AddSingleton<IQdrantClient>(sp =>
            {
                var client = new QdrantClient("localhost");
                return client;
            });

            return services;
        }
    }
}
