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
                Having rolled a series of dice, I obtained these results:
                    - d2Result: {diceSpread.D2}
                    - d4Result: {diceSpread.D4}
                    - d6Result: {diceSpread.D6}
                    - d8Result: {diceSpread.D8}
                    - d10_100Result: {diceSpread.D10_100}
                    - d12Result: {diceSpread.D12}
                    - d20Result: {diceSpread.D20}

                Create a marketing illustration inspired by dice divination results, with each dice outcome influencing the image differently:

                    d2Result (Masculinity/Femininity):
                        d2Result = 1: Masculine character.
                        d2Result = 2: Feminine character.
                    
                    d6Result (Age Group):
                        1-2: Child character.
                        3-5: Adult character.
                        6: Elder character.
                   
                    d8Result (Theme, 10x weight):
                        1: Incorporate tendrils or vines for a connection theme.
                        2: Dynamic scene with movement.
                        3: Lens effects for visual distortion.
                        4: Balance and symmetry.
                        5: Creation theme with a golem spirit or sacred object.
                        6: Focus on dramatic facial expressions, with a holographic x-ray effect.
                        7: Feature the all-seeing-eye.
                        8: Safe and secure environment.
                    
                    d12Result (Jungian Archetype):
                        1: Ruler's staff.
                        2: Creator's tools.
                        3: Sage's study.
                        4: Innocent's perspective (POV).
                        5: Explorer's world.
                        6: Rebel's enemy monster.
                        7: Hero's defeat.
                        8: Wizard creating sacred geometry.
                        9: Jester's humorous pose.
                        10: Everyman resting.
                        11: Lover with partner.
                        12: Caregiver with a child.

                    d10_100Result (Abstract vs. Concrete):
                        Lower values: Concrete, representational imagery.
                        Higher values: Abstract, symbolic imagery.
                    
                    First write a prompt for the image. Then check if it has characters. If there are characters, theyshould represent African, African American, or African Diaspora heritage. Any detail or themes in the image fit into the world of a fantasy version of Afro-Centric Folklore in the year 1888- in a cinematic style. Exclude dice and text-related elements from the image.
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