// Basic Configurations and Imports
using System.Text;
using System.Text.Json;
using Controllers;
using Dice.Context;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using ChatGPT;
using Astrology;
using RC.Moon;

// Load Environment Variables
DotEnv.Load();

// Configure WebApplication Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build WebApplication
var app = builder.Build();

// Setup Static Files and Default Files
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure Swagger for Development Environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS Redirection
app.UseHttpsRedirection();

// Route for Creating a New Dice Spread
app.MapPost("/createSpread", async (HttpContext context) =>
{
    Dictionary<String, object> spread = await DiceSpread.RollResults();
    return Results.Ok("New spread created successfully!");
})
.WithName("CreateSpread")
.WithOpenApi();

// Route for Interpreting a Specific Dice Spread
app.MapGet("/interpretDice/{id}", async (int id) =>
{
    using var context = new DiceContext();
    var diceSpread = await context.DiceSpread.FindAsync(id);

    if (diceSpread == null)
    {
        return Results.NotFound("DiceSpread not found.");
    }

    var chatGptRequest = ChatGPTController.FormatRequestForChatGPT(diceSpread);
    var chatGptResponse = await ChatGPTController.SendRequestToChatGPT(chatGptRequest);

    return Results.Ok(chatGptResponse);
})
.WithName("InterpretDice")
.WithOpenApi();

// Define a GET route to retrieve all dice rolls
app.MapGet("/getAllDiceRolls", async () =>
{
    using var context = new DiceContext();
    var allDiceRolls = await context.DiceSpread.ToListAsync();
    if (allDiceRolls == null || allDiceRolls.Count == 0)
    {
        return Results.NotFound("No dice rolls found.");
    }

    return Results.Ok(allDiceRolls);
})
.WithName("GetAllDiceRolls")
.WithOpenApi();

// Route for Getting All Dice Rolls in HTML Format
app.MapGet("/getAllDiceRollsDOM", async () =>
{
    using var context = new DiceContext();
    var allDiceRolls = await context.DiceSpread.OrderByDescending(roll => roll.Date).ToListAsync();

    if (allDiceRolls == null || allDiceRolls.Count == 0)
    {
        return Results.NotFound("No dice rolls found.");
    }

    var stringBuilder = new StringBuilder();
    foreach (var roll in allDiceRolls)
    {

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // This will make the deserialization case-insensitive.
        };

        Interpretation? interpretation = null;

        if (!string.IsNullOrEmpty(roll.Interpretation))
        {
            interpretation = JsonSerializer.Deserialize<Interpretation>(roll.Interpretation, options);
        }

        // Convert UTC to CST
        TimeZoneInfo cstZone;
        try
        {
            cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Windows time zone ID
        }
        catch (TimeZoneNotFoundException)
        {
            cstZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago"); // IANA time zone ID
        }
        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(roll.Date, cstZone);

        stringBuilder.AppendLine($"<div class='dice-roll-card'>");
        stringBuilder.AppendLine($"<h2>{cstTime.ToString("MMMM d, yyyy - h:mmtt")}</h2>");
        stringBuilder.AppendLine($"<span><b>ID: {roll.Id}</b></span>");
        stringBuilder.AppendLine("<hr>");
        stringBuilder.AppendLine("<div id='overview'>");
        stringBuilder.AppendLine("<h3>Overview</h3>");
        stringBuilder.AppendLine($"<p>{interpretation?.Overview_interpretation}</p>");
        stringBuilder.AppendLine("</div>");

        stringBuilder.AppendLine("<table id='resultTable'>");
        // Add table rows for each dice roll
        stringBuilder.AppendLine("<tr><th>Dice</th><th>Result</th><th>Interpretation</th></tr>");

        stringBuilder.AppendLine($"<tr><th>D2</th><td>{roll.D2}</td><td>{interpretation?.Dice_interpretations["d2"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D4</th><td>{roll.D4}</td><td>{interpretation?.Dice_interpretations["d4"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D6</th><td>{roll.D6}</td><td>{interpretation?.Dice_interpretations["d6"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D8</th><td>{roll.D8}</td><td>{interpretation?.Dice_interpretations["d8"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D12</th><td>{roll.D12}</td><td>{interpretation?.Dice_interpretations["d12"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D20</th><td>{roll.D20}</td><td>{interpretation?.Dice_interpretations["d20"]}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D10_100</th><td>{roll.D10_100}</td><td>{interpretation?.Dice_interpretations["d10_100"]}</td></tr>");
        stringBuilder.AppendLine("</table>");
        stringBuilder.AppendLine("</div>");
    }

    return Results.Content(stringBuilder.ToString(), "text/html");
})
.WithName("GetAllDiceRollsDOM")
.WithOpenApi();

// Route to GET lundar data for current day
app.MapGet("/getLunar/",  () =>
{
    Console.WriteLine("Getting lunar data...");
    Lunar lunar = new();
    Dictionary<string, object> currentMoonPhase = lunar.PrintCurrentMoonPhase();

    return Results.Ok(currentMoonPhase);
})
.WithName("GetLunar")
.WithOpenApi();

// Run the Server (Default: localhost:5036)
app.Run();
