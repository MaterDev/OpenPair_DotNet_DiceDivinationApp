//! --- 👾 Basic Configurations for app and swagger build ---

using System.Text;
using Controllers;
using Dice.Context;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup App Configuration
var app = builder.Build();

// Setup serving static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//! --- 👾 Routes ---

// Define a GET route to return weather
app.MapGet("/createSpread", () =>
{
    // Create 5 forecasts, and for each of them create a new WeatherForecast
    Dictionary<String, object> spread = DiceSpread.RollResults();
    return spread;
})
.WithName("CreateSpread")
.WithOpenApi();

//
app.MapGet("/interpretDice/{id}", async (int id) =>
{
    using var context = new DiceContext();
    Dice.Entities.DiceSpread? diceSpread = await context.DiceSpread.FindAsync(id);
    if (diceSpread == null)
    {
        return Results.NotFound("DiceSpread not found.");
    }

    // Format the dice spread into a request for ChatGPT
    var chatGptRequest = ChatGPTController.FormatRequestForChatGPT(diceSpread);
    Console.WriteLine(chatGptRequest);

    // Send the request to ChatGPT and get the response
    var chatGptResponse = await ChatGPTController.SendRequestToChatGPT(chatGptRequest);

    // return Results.Ok(chatGptResponse);
    return Results.Ok(chatGptResponse);
})
.WithName("InterpretDice")
.WithOpenApi(); ;

// Define a GET route to retrieve all dice rolls, returns htmx for the DOM
app.MapGet("/getAllDiceRolls", async () =>
{
    using var context = new DiceContext();
    var allDiceRolls = await context.DiceSpread.ToListAsync();
    if (allDiceRolls == null || allDiceRolls.Count == 0)
    {
        return Results.NotFound("No dice rolls found.");
    }

    var stringBuilder = new StringBuilder();
    foreach (var roll in allDiceRolls)
    {
        stringBuilder.AppendLine("<div class='dice-roll-card'>");
        stringBuilder.AppendLine("<h2>" + roll.Date.ToString("dd-MM-yyyy HH:mm") + "</h2>");
        stringBuilder.AppendLine("<span> ID: " + roll.Id + "</span>");

        stringBuilder.AppendLine("<hr>");

        stringBuilder.AppendLine("<table>");
        stringBuilder.AppendLine("<tr><th>D2</th><td>" + roll.D2 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D4</th><td>" + roll.D4 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D6</th><td>" + roll.D6 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D8</th><td>" + roll.D8 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D12</th><td>" + roll.D12 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D20</th><td>" + roll.D20 + "</td></tr>");
        stringBuilder.AppendLine("<tr><th>D100</th><td>" + roll.D10_100 + "</td></tr>");
        stringBuilder.AppendLine("</table>");
        stringBuilder.AppendLine("</div>");
    }

    return Results.Content(stringBuilder.ToString(), "text/html");
})
.WithName("GetAllDiceRolls")
.WithOpenApi();

//! Run the server, will run on localhost:5036
app.Run();
