using Asp.Net.Core._6.WebApi.Model;

namespace Asp.Net.Core._6.WebApi.Services;

public interface INewsService
{
    void Add(NewsModel newsModel);
    IEnumerable<NewsModel> Get();
}

public class NewsService : INewsService
{
    public NewsService()
    {
        newsModels = new();
    }

    private List<NewsModel> newsModels;

    public IEnumerable<NewsModel> Get()
    {
        return newsModels;
    }

    public void Add(NewsModel newsModel)
    {
        newsModels.Add(newsModel);
    }
}
