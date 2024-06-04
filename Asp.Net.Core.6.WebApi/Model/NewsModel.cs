namespace Asp.Net.Core._6.WebApi.Model;

public class NewsModel
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class NewsModelDto
{
    public string? Title { get; set; }
    public string? Body { get; set; }
}