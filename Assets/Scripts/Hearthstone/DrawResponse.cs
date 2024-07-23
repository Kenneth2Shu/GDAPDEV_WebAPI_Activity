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

    [JsonProperty("remaining")]
    public int Remaining { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
