namespace ChatGPT;

public class Interpretation
{
    public int Id { get; set; }
    public string? Overview_interpretation { get; set; }
    public Dictionary<string, string>? Dice_interpretations { get; set; }
}