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
    private string _baseURL = "https://omgvamp-hearthstone-v1.p.rapidapi.com/cards/sets/%7Bset%7D/";

    [SerializeField]
    private string _baseTextureURL = "https://art.hearthstonejson.com/v1/orig/";

    [SerializeField]
    private string _apiHost = "omgvamp-hearthstone-v1.p.rapidapi.com";

    [SerializeField]
    private string _apiKey = "86293ce88cmshf00e9dde1570aafp1efd59jsn85cf2b617e6a";

    [SerializeField]
    private TMP_Text _name;

    [SerializeField]
    private TMP_Text _attack;

    [SerializeField]
    private TMP_Text _health;

    [SerializeField]
    private Image _image;

    public void CreateDeck()
    {
        this.StartCoroutine(this.RequestDeck());
    }

    private IEnumerator RequestDeck() 
    {
        string url = this._baseURL + "cards/sets/Basic";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("X-RapidAPI-Key", _apiKey);
            request.SetRequestHeader("x-rapidapi-host", _apiHost);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            Debug.Log($"Response Code : {request.responseCode}");
            Debug.Log($"Json Response : {request.downloadHandler.text}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"Response : {request.downloadHandler.text}");

                List<DrawResponse> responseList = JsonConvert.DeserializeObject<List<DrawResponse>>(request.downloadHandler.text);

                List<DrawResponse> minionCards = responseList.FindAll(card => card.Type == "Minion");

                if (minionCards.Count > 0)
                {
                    DrawResponse randomMinion = minionCards[Random.Range(0, minionCards.Count)];
                    string imageURL = _baseTextureURL + randomMinion.CardId + ".png";
                    Debug.Log("[IMAGE] : " + imageURL);
                    StartCoroutine(DownloadTexture(imageURL));

                    _name.text = randomMinion.Name;
                    _attack.text = randomMinion.Attack.ToString();
                    _health.text = randomMinion.Health.ToString();
                }
                else
                {
                    Debug.Log("No minion card found.");
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

    void Awake() 
    {
        if(_instance == null){
            _instance = this;
        }
        else{
            Debug.Log("You should destroy yourself now.");
            Destroy(gameObject);
        }
    }
}
