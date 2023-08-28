using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Models;

public class CreateOrUpdateArticleRequestDto
{
    [MinLength(5)]
    [MaxLength(30)]
    [NotNull]
    public string? Headline { get; set; }
    
    [MaxLength(1000)]
    [NotNull]
    public string? Body { get; set; }
    
    [NotNull]
    public string? Author { get; set; }
    
    [NotNull]
    public string? ArticleImgUrl { get; set; }
}