namespace ChatGPT;

public class Interpretation
{
    public int Id { get; set; }
    public required string Overview_interpretation { get; set; }
    public required Dictionary<string, string> Dice_interpretations { get; set; }
}