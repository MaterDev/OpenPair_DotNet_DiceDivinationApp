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
            ""Having rolled a series of dice, I obtained these results: 
            D2: {diceSpread.D2}, D4: {diceSpread.D4}, D6: {diceSpread.D6}, 
            D8: {diceSpread.D8}, D10_100: {diceSpread.D10_100}, D12: {diceSpread.D12}, 
            D20: {diceSpread.D20}. Each dice roll holds a specific thematic significance, influencing the overall interpretation of its number. I seek a detailed interpretation based on numerology and gamatria, akin to a divination reading. The interpretation should offer practical insights and guidance tailored to my personal journey, decisions, or questions at hand.

            Please provide the interpretation in a JSON format with the following structure:

            - An 'overview_interpretation' key with a string value offering an overall guidance message. This should synthesize the collective meanings of the dice rolls, highlighting patterns, key areas to pay attention to, and unique attributes of the spread. It should provide an overarching summary relevant to my current life situation.

            - A 'dice_interpretations' key that is a dictionary. Each key should be the name of a dice ('d2', 'd4', 'd6', 'd8', 'd10_100', 'd12', 'd20'), with each value being a string that offers specific guidance. The interpretation for each dice should reflect its thematic association: 
                - 'd2' for fundamental choices or binary decisions,
                - 'd4' for stability or foundations,
                - 'd6' for balance and harmony in daily life,
                - 'd8' for material and financial aspects,
                - 'd10_100' for destiny and life's bigger picture,
                - 'd12' for completeness and spiritual wisdom,
                - 'd20' for unexpected challenges and opportunities.

            Each interpretation should provide actionable advice, insights, or warnings, addressing the respective thematic area, and tailored to personal growth, relationships, career, or spiritual matters, in line with numerology and gamatria principles.

            For example, the value for 'd2' should interpret the significance of the D2 roll, providing guidance for decision-making or pivotal life choices, based on its numerology and gamatria implications. Similarly, interpret the other dice rolls to offer focused, thematic guidance."";
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
