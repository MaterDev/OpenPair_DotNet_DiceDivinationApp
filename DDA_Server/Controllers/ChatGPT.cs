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

            - An 'overview_interpretation' key with a string value offering an overall guidance message. This should synthesize the collective meanings of the dice rolls, highlighting patterns and interesting mathematical relationships that may yeild some conceptual basis for symbolic interpretation, key areas to pay attention to. It should provide an overarching summary relevant to my current life situation.

            - A 'dice_interpretations' key that is a dictionary. Each key should be the name of a dice ('d2', 'd4', 'd6', 'd8', 'd10_100', 'd12', 'd20'), with each value being a string that offers specific guidance. The interpretation for each dice should reflect its thematic association: 
                - 'd2': for fundamental choices or binary decisions. A '1' may indicate that the overall reading is dominated by considerations that are of a spiritual nature, while a '2' may indicate that the overall reading is dominated by considerations that are of a material nature (As above, so below.)
                - 'd4': for stability or foundations. A '1' may indicate weak foundations, while a '4' may indicate strong foundations. Foundations are are the underlying basis for the current situation, and may be physical, emotional, mental, or spiritual things which are necessary for the current situation to exist/change/evolve.
                - 'd6': represents the key facets of one's path. The path is a set of tools for how one navigates their purpose/callin in the world.: '1' for the conjuration and manifestation as spiritual processes, '2' for the relationships and integrity, '3' for the warrior spirit and willpower, '4' for the the attainment of knowledge and wisdom, '5' for ego/instinct/primal-nature, and '6' for the higher-self and the connection to the Daimon. The interpretation should reflect the current state of each facet that corresponds to the result, and how it relates to the overall situation and advice.
                - 'd8': for state of consciousness which should be cultivated. '1' is the 'connected' state, '2' is the 'excited' state, '3' is the 'clear' state, '4' is the 'balanced' state, '5' is the 'formative' state, '6' is the 'focused' state, '7' is the 'introspective' state, '8' is the 'secure state' state. The interpretation should reflect the current state the result, and how it relates to the overall situation and advice.
                - 'd10_100': for destiny and life's bigger picture. The larger the number the more significant impact the reading will have for the immediate future. The lower the number the more significant impact the reading will have for the long-term future. The interpretation should reflect the result, and how it relates to the overall situation and advice.
                - 'd12': relates to the 12 Archetypes of Jungian psychology. This is psychological dice. '1' represents 'The Ruler', '2' represents 'The Creator', '3' represents 'The Sage', '4' represents 'The Innocent', '5' represents 'The Explorer', '6' represents 'The Rebel', '7' represents 'The Hero', '8' represents 'The Wizard', '9' represents 'The Jester', '10' represents 'The Everyman', '11' represents 'The Lover', '12' represents 'The Caregiver'. The archetype of the result should provide some quote, in the voice of the archtype, which is easy to remember and contemplate throughout the day. The interpretation should reflect the corresponding, and how it relates to the overall situation and advice. 
                - 'd20': is the difficult level of the over all reading. A lower number indicates relative difficulty in applying the advice, while a higher number indicates relative ease in applying the advice. The interpretation should reflect the result, and how it relates to the overall situation and advice. The goal of the D20 is to provide some forsight as to exactly what will be the most difficult, in the context of the overall reading. Assess this by considering the other dice rolls, and how they relate to the overall situation and advice.

            Each interpretation should provide actionable advice, insights, or warnings, addressing the respective thematic area, and tailored to personal growth, relationships, career, or spiritual matters, in line with numerology and gamatria principles.

            For example, the interpretation for 'd2' should provide direct guidance related to fundamental choices or binary decisions, reflecting its numerological and gamatria significance, without stating 'A roll of x on D2 indicates...'. The same approach should be followed for the other dice rolls, focusing on the thematic message rather than the mechanics of the dice."";
        ";

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
