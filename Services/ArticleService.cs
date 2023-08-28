namespace Services;
using Repository;
using Repository.Models;

public class ArticleService
{
    private readonly ArticleRepository _articleRepository;
    private readonly DataValidator _validator;
    
    public ArticleService(ArticleRepository articleRepository, DataValidator dataValidator)
    {
        _articleRepository = articleRepository;
        _validator = dataValidator;
    }
    
    public Article CreateArticle(CreateOrUpdateArticleRequestDto article)
    {
        if (_validator.AuthorValidator(article))
        {
            try
            {
                return _articleRepository.CreateArticle(article);
            }
            catch (Exception)
            {
                throw new Exception("Could not create article");
            }
        }
        throw new Exception("Author not valid");
    }
    
    public IEnumerable<NewsFeedItem> GetNewsFeed()
    {
        try
        {
            return _articleRepository.GetNewsFeed();
        }
        catch (Exception)
        {
            throw new Exception("Could not get articles");
        }
    }
    
    public Article GetArticle(int articleId)
    {
        try
        {
            return _articleRepository.GetArticle(articleId);
        }
        catch (Exception)
        {
            throw new Exception("Could not get article");
        }
    }
    
    public bool DeleteArticle(int articleId)
    {
        try
        {
            return _articleRepository.DeleteArticle(articleId);
        }
        catch (Exception)
        {
            throw new Exception("Could not delete article");
        }
    }
    
    public Article UpdateArticle(CreateOrUpdateArticleRequestDto article, int articleId)
    {
        if (_validator.AuthorValidator(article))
        {
            try
            {
                return _articleRepository.UpdateArticle(article, articleId);
            }
            catch (Exception)
            {
                throw new Exception("Could not update article");
            }
        }

        throw new Exception("Author not valid");
    }
    
    public IEnumerable<SearchArticleItem> SearchArticles(string searchTerm, int pageSize)
    {
        if (searchTerm.Length > 2 && pageSize > 0)
        {
            try
            {
                return _articleRepository.SearchArticles(searchTerm, pageSize);
            }
            catch (Exception)
            {
                throw new Exception("Could not search articles");
            }
        }

        throw new Exception("Search term must be at least 3 characters long");
    }
}