using Dice;
using Dice.Context;
using Newtonsoft.Json;

namespace Controllers;
public class DiceSpread
{
   public static async Task<Dictionary<String, object>> RollResults() {

        // Create all dice
        var d2 = new NewDice("d2");
        var d4 = new NewDice("d4");
        var d6 = new NewDice("d6");
        var d8 = new NewDice("d8");
        var d10_100 = new NewDice("d10_100");
        var d12 = new NewDice("d12");
        var d20 = new NewDice("d20");

        // Dictionary that stores KV pairs for diceType and rolls
        var diceRolls = new Dictionary<string, object> {
            {"d2", d2.Roll()},
            {"d4", d4.Roll()},
            {"d6", d6.Roll()},
            {"d8", d8.Roll()},
            {"d10_100", d10_100.Roll()},
            {"d12", d12.Roll()},
            {"d20", d20.Roll()},
            {"date", DateTime.UtcNow}
        };

        await WriteResults(diceRolls);
        return diceRolls;
    }

    private static async Task WriteResults(Dictionary<String, object> diceRolls)
        {

        using var context = new DiceContext();
        var diceSpread = new Dice.Entities.DiceSpread
        {
            D2 = (int)diceRolls["d2"],
            D4 = (int)diceRolls["d4"],
            D6 = (int)diceRolls["d6"],
            D8 = (int)diceRolls["d8"],
            D10_100 = (int)diceRolls["d10_100"],
            D12 = (int)diceRolls["d12"],
            D20 = (int)diceRolls["d20"],
            Date = DateTime.UtcNow,
        };

        var chatGptRequest = ChatGPTController.FormatRequestForChatGPT(diceSpread);
        var chatGptResponse = await ChatGPTController.SendRequestToChatGPT(chatGptRequest);
        string chatGptResponseJson = JsonConvert.SerializeObject(chatGptResponse);

        diceSpread.Interpretation = chatGptResponseJson;

        Console.WriteLine($"ChatGPT response: {chatGptResponse}");

        context.DiceSpread.Add(diceSpread);
        context.SaveChanges();
    }

}
