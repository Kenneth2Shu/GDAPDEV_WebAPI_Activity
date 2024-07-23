using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DeckResponse : MonoBehaviour{
    [JsonProperty("success")]
    public bool _success { get; set; }

    [JsonProperty("deck_ID")]
    public string _deckID { get; set; }

    [JsonProperty("remaining")]
    public int _remaining { get; set; }

    [JsonProperty("shuffled")]
    public bool _shuffled { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
