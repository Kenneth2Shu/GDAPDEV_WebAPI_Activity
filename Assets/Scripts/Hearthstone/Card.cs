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

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("attack")]
    public int Attack { get; set; }

    [JsonProperty("health")]
    public int Health { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
