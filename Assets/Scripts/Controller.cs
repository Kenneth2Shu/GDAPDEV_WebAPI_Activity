using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField]
    private string _cardState;

    [SerializeField]
    private bool runCreateDeck = false;

    [SerializeField]
    private GameObject _name;

    [SerializeField]
    private GameObject _hp;

    [SerializeField]
    private GameObject _attack;

    private void Start()
    {
        HearthstoneAPIManager.Instance.CreateDeck();
        this._cardState = "front";
    }

    private void Update()
    {
        if(runCreateDeck) {
            runCreateDeck = false;
            HearthstoneAPIManager.Instance.CreateDeck();
        }
    }

    public void OnFlip() {
        if(this._cardState == "front") {
            //
            this._attack.SetActive(false);
            this._hp.SetActive(false);
            this._name.SetActive(false);
            this._cardState = "back";
            HearthstoneAPIManager.Instance.FlipCard(this._cardState);
        }
        else if(this._cardState == "back") {
            //
            this._attack.SetActive(true);
            this._hp.SetActive(true);
            this._name.SetActive(true);
            this._cardState = "front";
            HearthstoneAPIManager.Instance.FlipCard(this._cardState);
        }
        else {
            this._cardState = "front";
            //error checking
        }
    }
}
