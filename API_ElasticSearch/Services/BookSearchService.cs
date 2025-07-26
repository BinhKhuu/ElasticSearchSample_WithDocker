using API_ElasticSearch.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace API_ElasticSearch.Services;

public interface IBookSearchService
{
    List<Book> GetBooks();
    Task<List<Book>> GetBooksAsync(string searchQuery);
    
}

public class BookSearchService : IBookSearchService
{
    private readonly ElasticsearchClient _client;
    public BookSearchService(
        ElasticsearchClient client
        )
    {
        _client = client;
    }

    public List<Book> GetBooks()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Book>> GetBooksAsync(string searchQuery)
    {
        var response = await _client.SearchAsync<Book>(s => s
            .Index("book")
            .Query(q => q
                .MultiMatch(m => m
                    .Query(searchQuery)
                    .Fields(new [] { "title", "longDescription","authors","shortDescription", "categories" })
                    .Type(TextQueryType.PhrasePrefix)
                    .Operator(Operator.Or)
                )
            )
        );

        return response.Documents.ToList();
    }
}