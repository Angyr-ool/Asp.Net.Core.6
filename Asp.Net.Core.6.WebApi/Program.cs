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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
