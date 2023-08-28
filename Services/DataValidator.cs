using Repository.Models;
namespace Services;

public class DataValidator
{
    public bool AuthorValidator(CreateArticleRequestDto article)
    {
        var authorList = new List<string> { "bob", "rob", "dob", "lob" };
        return article.Author != null && authorList.Contains(article.Author.ToLower());
    }
}