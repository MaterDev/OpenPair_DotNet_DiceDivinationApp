//! --- 👾 Basic Configurations for app and swagger build ---

using Controllers;

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


//! Run the server, will run on localhost:5036
app.Run();
