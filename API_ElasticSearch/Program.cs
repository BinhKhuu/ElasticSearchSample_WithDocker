using API_ElasticSearch.Services;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;

namespace API_ElasticSearch;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        ConfigureElasticsearchClientService(builder);
        builder.Services.AddScoped<IBookSearchService, BookSearchService>();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("LocalCorsPolicy", policy =>
            {
                policy
                    .WithOrigins(("http://localhost:4200"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseCors("LocalCorsPolicy");
        


        app.MapControllers();
        
        app.Run();
    }
    
    private static void ConfigureElasticsearchClientService(WebApplicationBuilder builder)
    {
        var elasticSearchHost = Environment.GetEnvironmentVariable("ELASTIC_SEARCH_HOST") ?? "localhost";
        var settings = new ElasticsearchClientSettings(new Uri($"http://{elasticSearchHost}:9200"));
        var client = new ElasticsearchClient(settings);
        builder.Services.AddSingleton(client);
    }
}