using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using ChatGPT;

namespace Controllers;
class ChatGPTController
{

    public static string FormatRequestForChatGPT(Dice.Entities.DiceSpread diceSpread)
    {
        string prompt = $@"
            ""I rolled a series of dice and obtained the following results: 
            D2: {diceSpread.D2}, D4: {diceSpread.D4}, D6: {diceSpread.D6}, 
            D8: {diceSpread.D8}, D10_100: {diceSpread.D10_100}, D12: {diceSpread.D12}, 
            D20: {diceSpread.D20}. Based on these results, I would like a numerological and gamatria interpretation. 
            Please provide the interpretation in a JSON format with the following structure:

            - An 'overview_interpretation' key with a string value that encapsulates the collective significance of all the dice rolls, 
            reflecting their combined numerological and gamatria insights.
            - A 'dice_interpretations' key that is a dictionary. In this dictionary, each key should be the name of a dice ('d2', 'd4', 'd6', 'd8', 'd10_100', 'd12', 'd20'), 
            and each value should be a string providing an individual interpretation for the roll of that specific dice, 
            based on numerology and gamatria concepts.

            For example, the value for 'd2' should interpret the significance of the D2 roll in the context of numerology and gamatria, and similarly for the other dice."";
        ";

        // Console.WriteLine($"Prompt: {prompt}");

        return prompt;
    }

    public static async Task<ChatGPTResponse> SendRequestToChatGPT(string request)
    {
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        var client = new OpenAIAPI(apiKey);

        try
        {
            var chatGptResponse = await client.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.GPT4_Turbo,
                Temperature = 0.1,
                MaxTokens = 1000,
                ResponseFormat = ChatRequest.ResponseFormats.JsonObject,
                Messages = new ChatMessage[] { new ChatMessage(ChatMessageRole.User, request) }
            });

            var responseContent = chatGptResponse.Choices.FirstOrDefault()?.Message?.TextContent;
            if (!string.IsNullOrEmpty(responseContent))
            {
                var responseObj = JsonConvert.DeserializeObject<ChatGPTResponse>(responseContent);
                return responseObj ?? new ChatGPTResponse();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return new ChatGPTResponse(); // Return an empty response object if no response or in case of an error
    }

}
