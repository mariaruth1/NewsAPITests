using Repository.Models;
namespace Services;

public class DataValidator
{
    public bool AuthorValidator(CreateOrUpdateArticleRequestDto article)
    {
        var authorList = new List<string> { "bob", "rob", "dob", "lob" };
        return authorList.Contains(article.Author.ToLower());
    }
}