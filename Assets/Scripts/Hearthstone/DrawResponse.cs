using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DrawResponse : MonoBehaviour {
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("deck_ID")]
    public string DeckID { get; set; }

    [JsonProperty("cards")]
    public List<Card> Cards { get; set; }

    [JsonProperty("minionData")]
    public List<MinionData> MinionDatas { get; set; }

    [JsonProperty("image")]
    public List<string> Image { get; set; }

    [JsonProperty("remaining")]
    public int Remaining { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
