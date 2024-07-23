using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HearthstoneAPIManager : MonoBehaviour {
    private static HearthstoneAPIManager _instance;
    public static HearthstoneAPIManager Instance {
        get { if (_instance == null) {}
            return _instance;
        }
    }

    [SerializeField]
    private string _baseURL = "https://omgvamp-hearthstone-v1.p.rapidapi.com/";

    [SerializeField]
    private string _baseTextureURL = "https://art.hearthstonejson.com/v1/orig/";

    [SerializeField]
    private string _apiHost = "omgvamp-hearthstone-v1.p.rapidapi.com";

    [SerializeField]
    private string _apiKey = "86293ce88cmshf00e9dde1570aafp1efd59jsn85cf2b617e6a";

    [SerializeField]
    private string _deckID;

    [SerializeField]
    private GameObject _cardObj1;

    [SerializeField]
    private DrawResponse _drawResponse;

    [SerializeField]
    private DeckResponse _deckResponse;

    public void CreateDeck() {
        this.StartCoroutine(this.RequestDeck());
    }

    private IEnumerator RequestDeck() {//coroutine for stuff that will take time
        string url = this._baseURL + "new/shuffle";
        using(UnityWebRequest request = new UnityWebRequest(url, "GET")) {//using whatever is in braces 
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Debug.Log($"Response Code : {request.responseCode}");
            Debug.Log($"Json Response : {request.downloadHandler.text}");

            if(string.IsNullOrEmpty(request.error)) {
                Debug.Log($"Response : {request.downloadHandler.text}");
                //Dictionary<string, string> response = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);

                DeckResponse response = JsonConvert.DeserializeObject<DeckResponse>(request.downloadHandler.text);
                this._deckResponse = response;
                this._deckID = response._deckID;
                Debug.Log("Success : " + response._success);
            }
            else {
                Debug.Log("Error : " + request.error);
            }
        }
        //yield return url;
    }

    public void Draw() {
        Debug.Log(this._deckResponse._remaining);
        if(this._deckResponse._remaining < 3) {
            Debug.LogWarning("Not enough cards remaining, reshuffling...");
            this.StartCoroutine(this.Reshuffle());
        }
        this.StartCoroutine(this.RequestDraw(1));
    }

    private IEnumerator Reshuffle() {
        string url = this._baseURL + this._deckID + "/shuffle/";
        using(UnityWebRequest request = new UnityWebRequest(url, "GET")) {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
        }
    }

    private IEnumerator RequestDraw(int nNum) {
        string url = this._baseURL + this._deckID + "/draw";
        using(UnityWebRequest request = new UnityWebRequest(url, "GET")) {//using whatever is in braces 
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Debug.Log($"Response Code : {request.responseCode}");
            Debug.Log($"Json Response : {request.downloadHandler.text}");

            if(string.IsNullOrEmpty(request.error)) {
                Debug.Log($"Response : {request.downloadHandler.text}");

                DrawResponse response = JsonConvert.DeserializeObject<DrawResponse>(request.downloadHandler.text);
                if(response.Success) {
                    string imageURL = response.Cards[0].Image;
                    //imageURL = "https://deckofcardsapi.com/static/img/6H.png";
                    Debug.Log("[IMAGE] : " + imageURL);
                    this.StartCoroutine(this.DownloadTexture(imageURL, nNum));
                }
            }
            else {
                Debug.Log("Error : " + request.error);
            }
        }
    }

    private IEnumerator DownloadTexture(string url, int nNum) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Your code serves ZERO purpose. You should delete gameObject NOW.");
        }
        else {
            DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
            Texture texture = response.texture;
            this._cardObj1.GetComponent<Renderer>().material.mainTexture = texture;
            
            //this._cardObj.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }
    }

    //private IEnumerator Request() {
    //    string url = this._baseURL + this._deckID + "/draw";
    //    using(UnityWebRequest request = new UnityWebRequest(url, "GET")) {
    //        request.SetRequestHeader("X-RapidAPI-Key", this._apiKey);
    //        request.SetRequestHeader("X-RapidAPI-Host", this._apiHost);
    //    }
    //}

    //List<> Clean(List<json object> response) {
        //filter so we only get json files with type minion
    //}


    void Awake() {
        if(_instance == null){
            _instance = this;
        }
        else{
            Debug.Log("You should destroy yourself now.");
            Destroy(gameObject);
        }
    }
}
