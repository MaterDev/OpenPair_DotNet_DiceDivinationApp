// Basic Configurations and Imports
using System.Text;
using Controllers;
using Dice.Context;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

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
app.MapPost("/createSpread", () =>
{
    Dictionary<String, object> spread = DiceSpread.RollResults();
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

// Route for Getting All Dice Rolls in HTML Format
app.MapGet("/getAllDiceRolls", async () =>
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
        stringBuilder.AppendLine($"<div class='dice-roll-card'>");
        stringBuilder.AppendLine($"<h2>{roll.Date:dd-MM-yyyy HH:mm}</h2>");
        stringBuilder.AppendLine($"<span> ID: {roll.Id}</span>");
        stringBuilder.AppendLine("<hr>");
        stringBuilder.AppendLine("<table>");

        // Add table rows for each dice roll
        stringBuilder.AppendLine($"<tr><th>D2</th><td>{roll.D2}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D4</th><td>{roll.D4}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D6</th><td>{roll.D6}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D8</th><td>{roll.D8}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D12</th><td>{roll.D12}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D20</th><td>{roll.D20}</td></tr>");
        stringBuilder.AppendLine($"<tr><th>D100</th><td>{roll.D10_100}</td></tr>");

        stringBuilder.AppendLine("</table>");
        stringBuilder.AppendLine("</div>");
    }

    return Results.Content(stringBuilder.ToString(), "text/html");
})
.WithName("GetAllDiceRolls")
.WithOpenApi();

// Run the Server (Default: localhost:5036)
app.Run();
