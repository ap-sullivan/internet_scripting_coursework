var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseDefaultFiles();

// Use so CSS hot-reloads
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
    }
});

app.MapRazorPages();

// Allow Azure to set the port
var port = Environment.GetEnvironmentVariable("PORT") ?? "5073";
app.Urls = new[] { $"http://*:{port}" };


app.Run();
