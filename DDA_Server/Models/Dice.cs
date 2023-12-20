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
    public string? TypeToUse {get; set;}
    private DiceType Type {get; set;}

    public void SetType() {
        Console.WriteLine($"TypeToUse: {Type}");
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
        Console.WriteLine($"Current type{Type}");
    }

    public int Roll() {
        Console.WriteLine($"Current type: {Type}");
        Random rnd = new Random();
        return Type switch {
            DiceType.D2 => rnd.Next(1, 3),
            DiceType.D4 => rnd.Next(1, 5),
            DiceType.D6 => rnd.Next(1, 7),
            DiceType.D8 => rnd.Next(1, 9),
            DiceType.D10_100 => rnd.Next(0, 101) - 1,
            DiceType.D12 => rnd.Next(1, 13),
            DiceType.D20 => rnd.Next(1, 21),
            _ => throw new InvalidOperationException("Dice type not set")
        };
    }

}