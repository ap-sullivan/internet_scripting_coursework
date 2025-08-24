var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Allow Azure to set the port
var port = Environment.GetEnvironmentVariable("PORT") ?? "5073";
builder.WebHost.UseUrls($"http://*:{port}"); // <-- moved here

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

app.Run();
