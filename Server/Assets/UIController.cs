using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject PlayScreen;
    public GameObject StartScreen;
    public GameObject BlueOn;
    public GameObject RedOn;
    public GameObject PlayButton;

    public GameObject Main;
    public GameObject DumbCamera;
    public GameObject LevelOne;
    public GameObject LevelTwo;
    public GameObject LevelThree;
    public GameObject LevelFour;


    public void StartPlayScreen()
    {
        PlayScreen.SetActive(true);
        StartScreen.SetActive(false);
    }
    public void BlueConnected()
    {
        BlueOn.SetActive(true);
    }
    public void RedConnected()
    {
        RedOn.SetActive(true);
    }
    public void ReadyToStart()
    {
        PlayButton.SetActive(true);
        Main.SetActive(false);
        LevelOne.SetActive(true);
        DumbCamera.SetActive(false);

        Player[] players = FindObjectsOfType<Player>();

        float startpos = 0;
        foreach (Player player in players)
        {
            
            player.transform.localPosition= new Vector3(-700 + startpos, -421f,0f);
            startpos+=100;
        }
    }

}
