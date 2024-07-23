using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Card : MonoBehaviour {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }
    
    [JsonProperty("images")]
    public Images Images { get; set; }
}
