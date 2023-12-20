//! --- 👾 Basic Configurations for app and swagger build ---

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Define a GET route to return weather
app.MapGet("/weatherforecast", () =>
{
    // Create 5 forecasts, and for each of them create a new WeatherForecast
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Will become the date, which is current day + index
            Random.Shared.Next(-20, 55), // Will choose a temp (c) from 20 - 55
            summaries[Random.Shared.Next(summaries.Length)] // choosing a random item from the array, accounting for the length of the array as the maximum index number to choose from.
        ))
        .ToArray(); // Casting a record into an array of values.
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


//! Run the server
app.Run();


//! This record defines the value schema for a weather forecast, when a new forecast is created the incoming parameters become the properties, and variables that are defined also become properties accessible. 
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}