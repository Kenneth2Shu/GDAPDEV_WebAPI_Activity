using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DrawResponse
{
    [JsonProperty("cardId")]
    public string CardId { get; set; }

    [JsonProperty("dbfId")]
    public int DbfId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("attack")]
    public int Attack { get; set; }

    [JsonProperty("health")]
    public int Health { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}
