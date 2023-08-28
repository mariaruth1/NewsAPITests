using Repository.Models;
using Npgsql;
using Dapper;

namespace Repository;

public class ArticleRepository
{
    private readonly NpgsqlDataSource _dataSource;
    
    public ArticleRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public Article CreateArticle(CreateOrUpdateArticleRequestDto orUpdateArticle)
    {
        var sql = $@"
INSERT INTO news.articles (headline, body, author, articleimgurl) 
VALUES (@headline, @body, @author, @articleImgUrl) RETURNING *;
";
        using var conn = _dataSource.OpenConnection();
        return conn.QuerySingle<Article>(sql, new {headline = orUpdateArticle.Headline, body = orUpdateArticle.Body, author = orUpdateArticle.Author, articleimgurl = orUpdateArticle.ArticleImgUrl});
    }
    
    //The landing page should display a list of recent news stories.
    //Each post should include a portion of the article text (body)
    //- a maximum of 51 characters, the headline, the image and the ID.
    //However, no author details should be present.
    //LEFT takes the first 50 characters of the body text starting from the left.
    public IEnumerable<NewsFeedItem> GetNewsFeed()
    {
        var sql = $@"
SELECT articleid, headline, LEFT(body, 50) AS body, articleimgurl FROM news.articles";
        using var conn = _dataSource.OpenConnection();
        return conn.Query<NewsFeedItem>(sql);
    }
    
    public Article GetArticle(int articleId)
    {
        var sql = $@"
SELECT * FROM news.articles WHERE articleid = @articleId;
";
        using var conn = _dataSource.OpenConnection();
        return conn.QuerySingle<Article>(sql, new { articleId });
    }

    public bool DeleteArticle(int articleId)
    {
        var sql = $@"
DELETE FROM news.articles WHERE articleid = @articleId;
";
        using var conn = _dataSource.OpenConnection();
        return conn.Execute(sql, new { articleId }) == 1;
    }
    
    public Article UpdateArticle(CreateOrUpdateArticleRequestDto article, int articleId)
    {
        var sql = $@"UPDATE news.articles SET headline = @headline, body = @body, author = @author, articleimgurl = @articleimgurl 
WHERE articleid = @articleId RETURNING *;
";
        using var conn = _dataSource.OpenConnection();
        return conn.QuerySingle<Article>(sql, new { headline = article.Headline, body = article.Body, author = article.Author, articleimgurl = article.ArticleImgUrl, articleId });
    }
    
    //Search for Articles: Users should be able to search for articles.
    //The client will include the search term and page size in the query parameters.
    //The search term should look for text matches in the body text.
    //Results may not exceed the page size.
    public IEnumerable<SearchArticleItem> SearchArticles(string searchTerm, int pageSize)
    {
        var sql = $@"
SELECT * FROM news.articles WHERE body LIKE @searchTerm LIMIT @pageSize;
";
        using var conn = _dataSource.OpenConnection();
        return conn.Query<SearchArticleItem>(sql, new { searchTerm = $"%{searchTerm}%", pageSize });
    }
}