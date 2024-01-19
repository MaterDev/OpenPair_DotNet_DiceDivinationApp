// Basic Configurations and Imports
using System.Text;
using System.Text.Json;
using Controllers;
using Dice.Context;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using ChatGPT;
using Astrology;
using System.Threading.Tasks.Dataflow;

// Load Environment Variables
DotEnv.Load();

// Configure WebApplication Builder
var builder = WebApplication.CreateBuilder(args);

// Allow CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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
app.MapPost("/api/createSpread", async (HttpContext context) =>
{
    Dictionary<String, object> spread = await DiceSpread.RollResults();
    return Results.Ok("New spread created successfully!");
})
.WithName("CreateSpread")
.WithOpenApi();

// Route for Interpreting a Specific Dice Spread
app.MapGet("/api/interpretDice/{id}", async (int id) =>
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
app.MapGet("/api/getAllDiceSpreads", async () =>
{
    using var context = new DiceContext();
    var allDiceRolls = await context.DiceSpread.ToListAsync();
    if (allDiceRolls == null || allDiceRolls.Count == 0)
    {
        return Results.NotFound("No dice rolls found.");
    }

    return Results.Ok(allDiceRolls);
})
.WithName("GetAllDiceSpreads")
.WithOpenApi();

// Route for Getting All Dice Rolls in HTML Format
app.MapGet("/api/getAllDiceSpreadsDOM", async () =>
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

        stringBuilder.AppendLine($"<div class='diceRollCard' id='rollCard-id-{roll.Id}' data-rollCard-id='{roll.Id}'>");

        stringBuilder.AppendLine($"<div class='rollCardHeader'>");
        stringBuilder.AppendLine($"<h2 class='diceRollCardTime'>{cstTime.ToString("MMMM d, yyyy - h:mmtt")}</h2>");
        stringBuilder.AppendLine($"<h3 class='diceRollCardID'>ID: {roll.Id}</h3>");
        stringBuilder.AppendLine($"</div>");

        stringBuilder.AppendLine($"<div class='rollCardOptions'>");

        stringBuilder.AppendLine($"<button class='rollImageBtn' id='rollImageBtn-{roll.Id}' data-cardid='{roll.Id}' onclick='rollImage(event)'>Roll Image</button>");


        stringBuilder.AppendLine($"</div>");



        LunarData? lunarData = null;
        // Add moon phase to card
        if (!string.IsNullOrEmpty(roll.LunarData))
        {
            lunarData = JsonSerializer.Deserialize<LunarData>(roll.LunarData, options);

            stringBuilder.AppendLine("<hr>");
            stringBuilder.AppendLine("<div class='lunarDataSection'>");
            stringBuilder.AppendLine($"<span class='lundarPhaseTxt'>{lunarData?.Phase}</span>");
            stringBuilder.AppendLine($"<span class='lundarPhaseEmoji'>{lunarData?.Moon_phase_emoji} - </span>");
            stringBuilder.AppendLine($"<span class='lundarZodiacTxt'>{lunarData?.Zodiac}</span>");
            stringBuilder.AppendLine($"<span class='lundarZodiacEmoji'>{lunarData?.Zodiac_emoji}</span>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("<hr>");
        }

        // Add Dalle3 image to card
        if (!string.IsNullOrEmpty(roll.Dalle3ImageUrl))
        {
            stringBuilder.AppendLine("<div class='dalle3Section'>");
            stringBuilder.AppendLine($"<img class='dalle3Img' src='{roll.Dalle3ImageUrl}' alt='Dalle3 Image'>");
            stringBuilder.AppendLine("</div>");
        }

        stringBuilder.AppendLine("<div class='overviewSection'>");
        stringBuilder.AppendLine("<h3 class='overviewTitle'>Overview</h3>");
        stringBuilder.AppendLine($"<p class='overviewText'>{interpretation?.Overview_interpretation}</p>");
        stringBuilder.AppendLine("</div>");

        stringBuilder.AppendLine("<table class='resultTable'>");
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
.WithName("GetAllDiceSpreadsDOM")
.WithOpenApi();

// Route to GET lundar data for current day
app.MapGet("/api/getLunar/", () =>
{
    Console.WriteLine("Getting lunar data...");
    Lunar lunar = new();
    Dictionary<string, object> currentMoonPhase = lunar.GetCurrentMoonPhase();

    return Results.Ok(currentMoonPhase);
})
.WithName("GetLunar")
.WithOpenApi();

// Route to get Dalle3 for a roll, by ID
app.MapPost("/api/createDalle3/{id}", async (int id) =>
{
    Console.WriteLine($"Creating Dalle3 for roll ID: {id}");
    using var context = new DiceContext();
    var diceSpread = await context.DiceSpread.FindAsync(id);

    if (diceSpread == null)
    {
        return Results.NotFound("DiceSpread not found.");
    }

    var dalle3Request = Dalle3Controller.FormattedRequestForDalle3(diceSpread);
    var dalle3Response = await Dalle3Controller.SendRequestToDalle3(dalle3Request, id);

    if (diceSpread != null)
    {
        // Update existing entity
        diceSpread.Dalle3ImageUrl = dalle3Response.ImageUrl;
        context.Entry(diceSpread).State = EntityState.Modified;
        try
        {
            // Save changes to the database
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Results.BadRequest("Something went wrong.");
        }

    }
    return Results.Ok(dalle3Response);

})
.WithName("CreateDalle3")
.WithOpenApi(); ;

// Run the Server (Default: localhost:5036)
app.Run();
