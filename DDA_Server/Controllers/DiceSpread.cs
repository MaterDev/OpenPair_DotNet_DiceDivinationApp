using Dice;

namespace Controllers;
class DiceSpread
{
   public static int RollResults() {

        var newDice = new NewDice{TypeToUse = "d4"};
        newDice.SetType();
        int newDiceResult = newDice.Roll();
        return newDiceResult;
    }
}
