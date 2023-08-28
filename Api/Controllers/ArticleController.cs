using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services;

namespace Api.Controllers;

[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ArticleService _articleService;
    
    public ArticleController(ArticleService articleService)
    {
        _articleService = articleService;
    }
    
    [HttpPost]
    [Route("/api/articles")]
    public Article CreateArticle([FromBody] CreateArticleRequestDto article)
    {
        return _articleService.CreateArticle(article);
    }
    
    /*[HttpGet]
    [Route("/api/feed")]
    public IEnumerable<Article> GetArticles()
    {
        return _articleService.GetAllArticles();
    }
    
    [HttpGet]
    [Route("/api/articles/{articleId}")]
    public Article GetArticle([FromRoute] int articleId)
    {
        return _articleService.GetArticle(articleId);
    }
    
    [HttpDelete]
    [Route("/api/articles/{articleId}")]
    public bool DeleteArticle([FromRoute] int articleId)
    {
        return _articleService.DeleteArticle(articleId);
    }
    
    [HttpPut]
    [Route("/api/articles/{articleId}")]
    public Article UpdateArticle([FromBody] Article article, [FromRoute] int articleId)
    {
        return _articleService.UpdateArticle(article, articleId);
    }*/
    
    /*Search for Articles: Users should be able to search for articles.
     The client will include the search term and page size in the query parameters. 
     The search term should look for text matches in the body text. 
     Results may not exceed the page size.

    Endpoint: GET http://localhost:5000/api/articles?searchterm=X&pagesize=X*/
}