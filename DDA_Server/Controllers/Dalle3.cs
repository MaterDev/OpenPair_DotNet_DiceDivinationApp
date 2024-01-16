namespace Controllers;
using Dalle3;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Images;
using OpenAI_API.Models;

class Dalle3Controller
{
    public static string FormattedRequestForDalle3(Dice.Entities.DiceSpread diceSpread)
    {

        if (diceSpread.Interpretation != null)
        {
            ChatGPT.ChatGPTResponse? diceInterpretation = diceSpread.Interpretation != null ? JsonConvert.DeserializeObject<ChatGPT.ChatGPTResponse>(diceSpread.Interpretation) : null;

            string prompt = $@"
                ""
                Create an image in square format that visually represents the following interpretation from a dice divination:

                The interpretation overview details is: {diceInterpretation?.OverviewInterpretation}

                Here are the dice interpretation details: {diceInterpretation?.DiceInterpretations}

                Details:

                Use the aformenteioned details from the interpretation overview and the dice interpretation details to create an image that visually represents the interpretation.

                First create an image prompt that represents the interpretation overview details. Then create an image prompt for each of the dice interpretation details. Then combine all of the image prompts into one image prompt. Then use the combined image prompt to create an image that visually represents the interpretation. This should be an image that incorporates which evever of the Jungian archetypes, in the context of details that can be gleamed from the prompt. Include both abstract and concrete elements.

                Pick a style, theme, color scheme, visual metaphor, and other details that are relevant. The common theme is that the image shuould have something to do with a Hoodoo Cross. Any characters in the image prompt should be specifically African American, Black Native, African, or African Diaspora. The general time period depicted in the image should be around 1888. Image doesnt need to necessarily be literal or realistic to the detailes provided.
                ""
                ";

            Console.WriteLine($"FormattedRequestForDalle3, prompt: {prompt}");
            return prompt;

        }
        Console.WriteLine("FormattedRequestForDalle3, diceSpread.Interpretation is null");
        return "An all red image with a big black x!!!!";
    }

    public static async Task<Dalle3Response> SendRequestToDalle3(string request)
    {

        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        var client = new OpenAIAPI(apiKey);

        try
        {
            var dalle3Response = await client.ImageGenerations.CreateImageAsync(
                new ImageGenerationRequest(
                    request,
                    OpenAI_API.Models.Model.DALLE3,
                    ImageSize._1024,
                    "hd"
                    )
                );

            Console.WriteLine($"Dalle3 response: {dalle3Response.Data[0].Url}");

            var responseContent = dalle3Response.Data[0].Url;
            return new Dalle3Response
            {
                ImageUrl = responseContent
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }

        return new Dalle3Response();
    }
}