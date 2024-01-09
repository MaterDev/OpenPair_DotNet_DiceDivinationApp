namespace ChatGPT;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ChatGPTResponse
{
    [Key]
    public int Id { get; set; }
    
    [JsonProperty("overview_interpretation")]
    public string? OverviewInterpretation { get; set; }

    [JsonProperty("dice_interpretations")]
    public Dictionary<string, string> DiceInterpretations { get; set; }

    public ChatGPTResponse()
    {
        DiceInterpretations = new Dictionary<string, string>();
    }
}
