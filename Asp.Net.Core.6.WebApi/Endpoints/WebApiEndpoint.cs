using Asp.Net.Core._6.WebApi.Model;
using Asp.Net.Core._6.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net.Core._6.WebApi.Endpoints;

public static class WebApiEndpoint
{
    private static string BaseUrl = "api/news";

    public static void MapWebApiService(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseUrl, async (context) =>
        {
            var newsService = context.RequestServices.GetService<INewsService>();
            await context.Response.WriteAsJsonAsync(newsService?.Get());
        });

        app.MapPost(BaseUrl, (HttpContext context, [FromBody] NewsModelDto newsModelDto) =>
        {
            var newsService = context.RequestServices.GetService<INewsService>();
            var newsModel = new NewsModel()
            {
                Title = newsModelDto.Title,
                Body = newsModelDto.Body,
                CreatedAt = DateTime.Now,
            };
            newsService?.Add(newsModel);
        });

        app.MapGet($"{BaseUrl}/{{first}}/{{second}}/{{third}}", async (context) =>
        {
            var firstSegment = context.Request.RouteValues["first"];
            var secondSegment = context.Request.RouteValues["second"];
            var thirdSegment = context.Request.RouteValues["third"];

            await context.Response.WriteAsync($"tripple_segment: {firstSegment}/{secondSegment}/{thirdSegment}");
        });

        app.MapGet($"{BaseUrl}/files/{{filename}}.{{ext}}", async (context) =>
        {
            await context.Response.WriteAsync("filename");
        });
    }
}
