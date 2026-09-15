using Microsoft.EntityFrameworkCore;
using Thallo.Data;
using Thallo.Models;
using System.Text.Json;
using System.Net.Http.Json;

namespace Thallo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ThalloContext>(options =>
            options.UseSqlite("Data Source=thallo.db"));
        builder.Services.AddHttpClient();
        var app = builder.Build();

        // seed default shortcuts on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ThalloContext>();
    if (!db.Shortcuts.Any())
    {
        db.Shortcuts.AddRange(
            new Shortcut { Label="Coffee",   Type="caffeine", Icon="☕", Value=80,  Unit="mg", Color="#e0a155", Aggregation="sum",   ShowTally=true,  SortOrder=1 },
            new Shortcut { Label="Tea",      Type="caffeine", Icon="🍵", Value=40,  Unit="mg", Color="#e0a155", Aggregation="sum",   ShowTally=true,  SortOrder=2 },
            new Shortcut { Label="Water",    Type="water",    Icon="💧", Value=250, Unit="ml", Color="#56c5d8", Aggregation="sum",   ShowTally=true,  SortOrder=3 },
            new Shortcut { Label="Bottle",   Type="water",    Icon="🚰", Value=500, Unit="ml", Color="#56c5d8", Aggregation="sum",   ShowTally=true,  SortOrder=4 },
            new Shortcut { Label="Meal",     Type="food",     Icon="🍽️", Value=null, Unit=null, Color="#93d56f", Aggregation="count", ShowTally=true,  SortOrder=5 },
            new Shortcut { Label="Snack",    Type="food",     Icon="🥗", Value=null, Unit=null, Color="#93d56f", Aggregation="count", ShowTally=true,  SortOrder=6 },
            new Shortcut { Label="Cigarette",Type="cigarette",Icon="🚬", Value=null, Unit=null, Color="#b9885a", Aggregation="count", ShowTally=true,  SortOrder=7 },
            new Shortcut { Label="Bathroom", Type="bathroom", Icon="🚽", Value=null, Unit=null, Color="#7e9b8e", Aggregation="count", ShowTally=false, SortOrder=8 }
        );
        db.SaveChanges();
    }
}
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapPost("/log", async (LogRequest req, ThalloContext db) =>
        {
            var entry = new LogEntry
{
    Timestamp = DateTime.UtcNow,
    Type = req.Type,
    Value = req.Value,
    Message = req.Message,
    Calories = req.Calories,
    Sugar = req.Sugar,
    Salt = req.Salt,
    Protein = req.Protein
};
            db.Logs.Add(entry);
            await db.SaveChangesAsync();
            return Results.Ok(entry);
        });
        // list all shortcuts, ordered
app.MapGet("/shortcuts", async (ThalloContext db) =>
    await db.Shortcuts.OrderBy(s => s.SortOrder).ThenBy(s => s.Id).ToListAsync());

// create a shortcut
app.MapPost("/shortcuts", async (Shortcut s, ThalloContext db) =>
{
    db.Shortcuts.Add(s);
    await db.SaveChangesAsync();
    return Results.Ok(s);
});
//imports
app.MapGet("/daily", async (ThalloContext db) =>
    await db.DailyHealth
        .OrderByDescending(d => d.Date)
        .Take(30)
        .ToListAsync());

// full history for the calendar view
app.MapGet("/daily/all", async (ThalloContext db) =>
    await db.DailyHealth
        .OrderByDescending(d => d.Date)
        .ToListAsync());

// delete a shortcut
app.MapDelete("/shortcuts/{id}", async (int id, ThalloContext db) =>
{
    var s = await db.Shortcuts.FindAsync(id);
    if (s is null) return Results.NotFound();
    db.Shortcuts.Remove(s);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
        app.MapDelete("/log/{id}", async (int id, ThalloContext db) =>
{
    var entry = await db.Logs.FindAsync(id);
    if (entry is null) return Results.NotFound();
    db.Logs.Remove(entry);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapPost("/parse", async (ParseRequest req, IHttpClientFactory http) =>
{
    var key = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
    if (string.IsNullOrEmpty(key)) return Results.Problem("No API key set");

    var prompt = $$"""
    Estimate nutrition for this food/drink log entry: "{{req.Text}}"
    Respond with ONLY a JSON object, no markdown, no prose:
    {"calories": number, "caffeine_mg": number, "sugar_g": number, "salt_g": number, "protein_g": number, "summary": "short label"}
    Use 0 for any value that doesn't apply. Best single estimate for a typical portion.
    """;

    var body = new
    {
        contents = new[] { new { parts = new[] { new { text = prompt } } } },
        generationConfig = new { responseMimeType = "application/json" }
    };

    var client = http.CreateClient();
    var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={key}";
    var resp = await client.PostAsJsonAsync(url, body);
    var json = await resp.Content.ReadFromJsonAsync<JsonElement>();

    var text = json.GetProperty("candidates")[0]
                   .GetProperty("content").GetProperty("parts")[0]
                   .GetProperty("text").GetString();

    return Results.Content(text ?? "{}", "application/json");
});

        app.MapGet("/logs", async (ThalloContext db) =>
            await db.Logs
                .OrderByDescending(l => l.Timestamp)
                .Take(50)
                .ToListAsync());

        app.Run();
    }   // <- closes Main
}       // <- closes Program

// LogRequest lives at namespace level, outside Program
public class LogRequest
{
    public string Type { get; set; } = string.Empty;
    public double? Value { get; set; }
    public string? Message { get; set; }
    public double? Calories { get; set; }
    public double? Sugar { get; set; }
    public double? Salt { get; set; }
    public double? Protein { get; set; }
}
public record ParseRequest(string Text);