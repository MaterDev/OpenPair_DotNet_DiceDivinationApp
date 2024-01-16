namespace Dalle3;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class Dalle3Response
{
    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }

}
