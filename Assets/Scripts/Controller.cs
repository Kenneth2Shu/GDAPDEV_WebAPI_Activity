using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField]
    private bool runCreateDeck = false;

    [SerializeField]
    private bool runDrawCard = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(runCreateDeck) {
            runCreateDeck = false;
            HearthstoneAPIManager.Instance.CreateDeck();
        }
        if(runDrawCard) {
            runDrawCard = false;
            HearthstoneAPIManager.Instance.Draw();
        }
    }
}
