using Asp.Net.Core._6.WebApi.Endpoints;
using Asp.Net.Core._6.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(opts =>
{
    if (builder.Environment.IsDevelopment())
    {
        opts.Path = "appsettings.Development.json";
    }
    else if (builder.Environment.IsStaging())
    {
        opts.Path = "appsettings.Staging.json";
    }
    else
    {
        opts.Path = "appsettings.json";
    }

    opts.Optional = false;
    opts.ReloadOnChange = false;
});

builder.Services.AddSingleton<INewsService, NewsService>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => 
{
    endpoints.MapGet("/", () => "ASP.NET Core Web API");
    endpoints.MapWebApiService();
});

app.Run();
