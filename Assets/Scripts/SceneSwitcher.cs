using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwtichToCardScene()
    {
        SceneManager.LoadScene("CardScene");
    }

    public void SwitchToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
