using Controllers;
using Newtonsoft.Json;
using OpenAI_API;

namespace Controllers;
class ChatGPTController
{

    public static string FormatRequestForChatGPT(Dice.Entities.DiceSpread diceSpread)
{
    // Format your dice spread into a meaningful request for ChatGPT
    return $"I rolled a series of dice with the following results: D2: {diceSpread.D2}, D4: {diceSpread.D4}, ... Please provide a numerological interpretation.";
}


//    private static async Task<string> SendRequestToChatGPT(string request)
// {
//     var apiKey = "Your_OpenAI_API_Key";
//     var client = new OpenAIAPI(apiKey);

//     var chatGptResponse = await client.Completions.CreateAsync(request);

//     // Parse the response into the desired JSON format
//     var jsonResponse = new
//     {
//         interpretation = chatGptResponse.ToString(), // Or extract specific parts of the response
//         advice = "Some advice based on the response" // You might need to parse the response to generate advice
//     };

//     return JsonConvert.SerializeObject(jsonResponse);
// }

}
