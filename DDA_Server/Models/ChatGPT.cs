namespace ChatGPT;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ChatGPTResponse
{
    [JsonProperty("overview_interpretation")]
    public string? OverviewInterpretation { get; set; }

    [JsonProperty("dice_interpretations")]
    public Dictionary<string, string> DiceInterpretations { get; set; }

    public ChatGPTResponse()
    {
        DiceInterpretations = new Dictionary<string, string>();
    }
}
