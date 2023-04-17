using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject PlayScreen;
    public void StartGame()
    {
        GameObject StartScreen = GameObject.Find("StartScreen");
        StartScreen.SetActive(false);

        //GameObject PlayScreen = GameObject.Find("PlayScreen");
        PlayScreen.SetActive(true);
    }
    public void StartLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }
}
