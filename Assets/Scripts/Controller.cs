using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField]
    private bool runCreateDeck = false;

    private void Start()
    {
        HearthstoneAPIManager.Instance.CreateDeck();
    }

    private void Update()
    {
        if(runCreateDeck) {
            runCreateDeck = false;
            HearthstoneAPIManager.Instance.CreateDeck();
        }
    }
}
