using System;
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

    private IEnumerator coroutine;
    public CanvasGroup canvasDaDa;

    public bool fadeIn = false;
    public bool fadeOut = false;

    private void Start()
    {
        //coroutine = FadeOut();
    }
    private void Update()
    {
        if(fadeIn) 
        {
            if(canvasDaDa.alpha< 1.0f)
            {
                canvasDaDa.alpha += Time.deltaTime;
                if(canvasDaDa.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            if (canvasDaDa.alpha >=0f)
            {
                canvasDaDa.alpha -= Time.deltaTime;
                if (canvasDaDa.alpha == 1)
                {
                    fadeOut = false;
                }
            }
        }
    }
    void FadeIn()
    {
        LeanTween.value(0, 1, 1).setOnUpdate((float x)  => { 
            canvasDaDa.alpha = x;
        });
    }
    void FadeOut()
    {
        LeanTween.value(1, 0, 1).setOnUpdate((float x) => {
            canvasDaDa.alpha = x;
        });
    }
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
    public void Ready()
    {
        PlayButton.SetActive(true);
    }
    public void ReadyToStart()
    {
        FadeOut();
        StartCoroutine(Waiter(1, Bliat));
    }
    void Bliat()
    {
        Main.SetActive(false);
        LevelOne.SetActive(true);
        DumbCamera.SetActive(false);
        Player[] players = FindObjectsOfType<Player>();

        float startpos = 0;
        foreach (Player player in players)
        {

            player.transform.localPosition = new Vector3(-700 + startpos, -421f, 0f);
            startpos += 100;
        }
        FadeIn();
    }
    public void GoToLevel2()
    {
        FadeOut();
        StartCoroutine(Waiter(1, lvl2));
        
    }
    void lvl2()
    {
        LevelOne.SetActive(false);
        LevelTwo.SetActive(true);
        
        Player[] players = FindObjectsOfType<Player>();

        float startpos = 0;
        foreach (Player player in players)
        {

            player.transform.localPosition = new Vector3(-35 + startpos, -421f, 0f);
            startpos += 100;
        }
        FadeIn();
    }
    public void GoToLevel3()
    {
        FadeOut();
        StartCoroutine(Waiter(1, lvl3));
        
    }
    void lvl3()
    {
        LevelThree.SetActive(true);
        LevelTwo.SetActive(false);

        Player[] players = FindObjectsOfType<Player>();

        float startpos = 0;
        foreach (Player player in players)
        {

            player.transform.localPosition = new Vector3(-812 + startpos, 158f, 0f);
            startpos += 100;
        }
        FadeIn();
    }
    public void GoToLevel4()
    {
        FadeOut();
        StartCoroutine(Waiter(1, lvl4));
    }
    void lvl4()
    {
        LevelThree.SetActive(false);
        LevelFour.SetActive(true);

        Player[] players = FindObjectsOfType<Player>();

        float startpos = 0;
        foreach (Player player in players)
        {

            player.transform.localPosition = new Vector3(-805 + startpos, 320f, 0f);
            startpos += 100;
        }
        FadeIn();
    }
    IEnumerator Waiter(float time, Action method)
    {
        yield return new WaitForSeconds(time);
        method.Invoke();
    }
    //IEnumerator FadeOut()
    //{
    //    yield return new WaitForSeconds(2f);
    //}
}
