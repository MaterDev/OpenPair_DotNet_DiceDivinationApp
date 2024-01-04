//! --- 👾 Basic Configurations for app and swagger build ---

using Controllers;
using Dice.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//! --- 👾 Routes ---

// Define a GET route to return weather
app.MapGet("/getSpread", () =>
{
    // Create 5 forecasts, and for each of them create a new WeatherForecast
    Dictionary<String, object> spread = DiceSpread.RollResults();
    return spread;
})
.WithName("GetSpread")
.WithOpenApi();

app.MapGet("/interpretDice/{id}", async (int id) =>
{
    using (var context = new DiceContext())
    {
        Dice.Entities.DiceSpread diceSpread = await context.DiceSpread.FindAsync(id);
        if (diceSpread == null)
        {
            return Results.NotFound("DiceSpread not found.");
        }

        // Format the dice spread into a request for ChatGPT
        var chatGptRequest = ChatGPTController.FormatRequestForChatGPT(diceSpread);
        Console.WriteLine(chatGptRequest);
        
        // Send the request to ChatGPT and get the response
        // var chatGptResponse = await SendRequestToChatGPT(chatGptRequest);

        // return Results.Ok(chatGptResponse);
        return Results.Ok(diceSpread);
    }
});


//! Run the server, will run on localhost:5036
app.Run();
