using Dice;

namespace Controllers;
class DiceSpread
{
   public static Dictionary<String, int> RollResults() {

        var d2 = new NewDice("d2");
        var d4 = new NewDice("d4");
        var d6 = new NewDice("d6");
        var d8 = new NewDice("d8");
        var d10_100 = new NewDice("d10_100");
        var d12 = new NewDice("d12");
        var d20 = new NewDice("d20");

        // Dictionary that stores KV pairs for diceType and rolls
        var diceRolls = new Dictionary<string, int> {
            {"d2", d2.Roll()},
            {"d4", d4.Roll()},
            {"d6", d6.Roll()},
            {"d8", d8.Roll()},
            {"d10_100", d10_100.Roll()},
            {"d12", d12.Roll()},
            {"d20", d20.Roll()},
        };

        return diceRolls;
    }
}
