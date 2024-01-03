namespace Dice;

public enum DiceType {
    D2,
    D4,
    D6,
    D8,
    D10_100,
    D12,
    D20
}

class NewDice {
    public string? TypeToUse {get; private set;}
    private DiceType Type {get; set;}
    public int RollResult {get; private set;} // Public property to hold the result of a dice

// When new dice is created will automatically set the type and do a roll.
    public NewDice(string typeToUse) {
        TypeToUse = typeToUse;
        SetType();
        RollResult = Roll();
    }

    // Will set the type of dice that this is intended to be, which will have consequences for subsequent functionality.
    public void SetType() {
        Type = TypeToUse switch {
            "d2" => DiceType.D2,
            "d4" => DiceType.D4,
            "d6" => DiceType.D6,
            "d8" => DiceType.D8,
            "d10_100" => DiceType.D10_100,
            "d12" => DiceType.D12,
            "d20" => DiceType.D20,
            _ => throw new ArgumentException("Invalid Dice Type")
        };
    }

    // Uses DiceType to determine which roll should be performed.
    public int Roll() {
        Random rnd = new Random();
        return Type switch {
            // The upper ranger is non inclusive
            DiceType.D2 => rnd.Next(1, 3),
            DiceType.D4 => rnd.Next(1, 5),
            DiceType.D6 => rnd.Next(1, 7),
            DiceType.D8 => rnd.Next(1, 9),
            DiceType.D10_100 => rnd.Next(0, 101),
            DiceType.D12 => rnd.Next(1, 13),
            DiceType.D20 => rnd.Next(1, 21),
            _ => throw new InvalidOperationException("Dice type not set")
        };
    }

}