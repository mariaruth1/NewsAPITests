using System.ComponentModel.DataAnnotations;

namespace Repository.Models;

public class Article
{
    public int? ArticleId { get; set; }
    [MinLength(56)]
    public string? Headline { get; set; }
    public string? Body { get; set; }
    public string? Author { get; set; }
    public string? ArticleImgUrl { get; set; }
}