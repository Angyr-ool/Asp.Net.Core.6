using Asp.Net.Core._6.WebApi.Endpoints;
using Asp.Net.Core._6.WebApi.Services;
using Microsoft.OpenApi.Models;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Web_API",
        Version = "v1",
    });
});

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => 
{
    endpoints.MapGet("/", () => "ASP.NET Core 6 Web API");
    endpoints.MapWebApiService();
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(options => 
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web_API");
});

app.Run();
