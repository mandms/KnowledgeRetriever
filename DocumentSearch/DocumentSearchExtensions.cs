using Microsoft.Extensions.DependencyInjection;
using Qdrant.Client;

namespace DocumentSearch
{
    public static class DocumentSearchExtensions
    {
        public static IServiceCollection AddDocumentSearch(this IServiceCollection services)
        {
            services.AddSingleton<IQdrantClient>(sp =>
            {
                var client = new QdrantClient("localhost");
                return client;
            });

            services.AddScoped<IQdrantService, QdrantService>();

            return services;
        }
    }
}
