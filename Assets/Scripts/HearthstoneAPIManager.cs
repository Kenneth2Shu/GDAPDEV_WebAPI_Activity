using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

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
    private TMP_Text _name;

    [SerializeField]
    private TMP_Text _attack;

    [SerializeField]
    private TMP_Text _health;

    [SerializeField]
    private DrawResponse _drawResponse;

    [SerializeField]
    private DeckResponse _deckResponse;

    [SerializeField]
    private Image _image;

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
        this.StartCoroutine(this.Reshuffle());
        Debug.Log(this._deckResponse._remaining);
        this.StartCoroutine(this.RequestDraw());
    }

    private IEnumerator Reshuffle() {
        string url = this._baseURL + this._deckID + "/shuffle/";
        using(UnityWebRequest request = new UnityWebRequest(url, "GET")) {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
        }
    }

    private IEnumerator RequestDraw()
    {
        string url = _baseURL + "cards/sets/Basic";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("x-rapidapi-key", _apiKey);
            request.SetRequestHeader("x-rapidapi-host", _apiHost);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            Debug.Log($"Response Code : {request.responseCode}");
            Debug.Log($"Json Response : {request.downloadHandler.text}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"Response : {request.downloadHandler.text}");

                DrawResponse response = JsonConvert.DeserializeObject<DrawResponse>(request.downloadHandler.text);
                if (response.Success)
                {
                    string imageURL = _baseTextureURL + "BG_CS2_200_G" + ".png";
                    Debug.Log("[IMAGE] : " + imageURL);
                    StartCoroutine(DownloadTexture(imageURL));
                }
            }
            else
            {
                Debug.Log("Error : " + request.error);
            }
        }
    }


    private IEnumerator DownloadTexture(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error downloading texture: " + request.error);
            }
            else
            {
                DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
                Texture2D texture = response.texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _image.sprite = sprite;
            }
        }
    }

    Card Clean(DrawResponse response) {
        //filter so we only get json files with type minion
        bool isMinion = false;
        Card chosenMinion = new Card();

        for(int i = 0; i < response.MinionDatas.Count || isMinion == false; i++) {
            if(response.MinionDatas[i].Type == "Minion") {
                isMinion = true;
                chosenMinion.MinionDatas = response.MinionDatas[i];
                chosenMinion.Code = response.MinionDatas[i].Code;
                chosenMinion.Image = response.Image[i];
            }
        }

        return chosenMinion;
    }


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
