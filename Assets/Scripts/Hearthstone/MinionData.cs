using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MinionData {
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("cardId")]
    public string CardId;

    [JsonProperty("name")]
    public string Name;

    [JsonProperty("type")]
    public string Type;

    [JsonProperty("attack")]
    public int Attack;

    [JsonProperty("health")]
    public int Health;
}
