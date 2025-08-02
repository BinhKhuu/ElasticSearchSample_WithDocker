using System.Text.Json;
using API_ElasticSearch.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TEST_ElasticSearch.IntegrationTests.Controllers;

public class BookControllerTest : IClassFixture<WebApplicationFactory<API_ElasticSearch.Program>>
{
    private readonly HttpClient _client;

    public BookControllerTest(WebApplicationFactory<API_ElasticSearch.Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_Books_ShouldReturnAllBooks()
    {
        var response = await _client.GetAsync("/api/book?query=test");
        var stringResponse = await response.Content.ReadAsStringAsync();
        var books = JsonSerializer.Deserialize<List<Book>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        response.EnsureSuccessStatusCode();
        Assert.NotEmpty(stringResponse);
        Assert.NotNull(books);
    }

    // Issue ElasticSearch is a Service that needs to run those this test to pass being an integration test it should run BUT is it possible to run this within a CICD Pipeline like azure or will the localhostSettings prevent docker from running correctly
    [Fact]
    async Task GetSearch_ShouldFindController()
    {
        var response = await _client.GetAsync("/api/book/search?query=test");
        var stringResponse = await response.Content.ReadAsStringAsync();
        var books = JsonSerializer.Deserialize<List<Book>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        response.EnsureSuccessStatusCode();
        Assert.NotEmpty(stringResponse);
        Assert.NotNull(books);
    }
}