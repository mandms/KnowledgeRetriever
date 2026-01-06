using Microsoft.Extensions.DependencyInjection;
using Qdrant.Client;
using System.Net.Http.Headers;

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
            services.AddHttpClient<ILLMService, LLMService>();
            services.AddScoped<ISearch, Search>();

            services.ConfigureHttpClientDefaults(conf => conf.ConfigureHttpClient(conf => {
                conf.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                conf.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }));

            return services;
        }
    }
}
