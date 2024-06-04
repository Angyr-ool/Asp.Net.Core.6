using Asp.Net.Core._6.WebApi.Model;
using Asp.Net.Core._6.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net.Core._6.WebApi.Endpoints;

public static class WebApiEndpoint
{
    private static string BaseUrl = "api/news";

    public static void MapWebApiService(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseUrl, () =>
        {
            var newsService = app.ServiceProvider.GetService<INewsService>();
            return newsService?.Get();
        })
        .WithName("GetAllNewsModels")
        .Produces<IEnumerable<NewsModel>?>(StatusCodes.Status200OK);

        app.MapPost(BaseUrl, ([FromBody] NewsModelDto newsModelDto) =>
        {
            var newsService = app.ServiceProvider.GetService<INewsService>();
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

	/*public static void MapNewsModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/NewsModel", () =>
        {
            return new [] { new NewsModel() };
        })
        .WithName("GetAllNewsModels")
        .Produces<NewsModel[]>(StatusCodes.Status200OK);

        routes.MapGet("/api/NewsModel/{id}", (int id) =>
        {
            //return new NewsModel { ID = id };
        })
        .WithName("GetNewsModelById")
        .Produces<NewsModel>(StatusCodes.Status200OK);

        routes.MapPut("/api/NewsModel/{id}", (int id, NewsModel input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateNewsModel")
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/NewsModel/", (NewsModel model) =>
        {
            //return Results.Created($"//api/NewsModels/{model.ID}", model);
        })
        .WithName("CreateNewsModel")
        .Produces<NewsModel>(StatusCodes.Status201Created);

        routes.MapDelete("/api/NewsModel/{id}", (int id) =>
        {
            //return Results.Ok(new NewsModel { ID = id });
        })
        .WithName("DeleteNewsModel")
        .Produces<NewsModel>(StatusCodes.Status200OK);
    }*/
}
