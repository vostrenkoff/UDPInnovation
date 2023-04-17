using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Player;

public class NextLevelTriggerScript : MonoBehaviour
{
    public bool player1InTrigger;
    public bool player2InTrigger;
    UIController UIcontroller;
    [SerializeField] public Levels GoToLevel;
    public enum Levels
    {
        Main,
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour,
        LevelFive
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InTrigger = true;
        }
        else if (other.CompareTag("Player2"))
        {
            player2InTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InTrigger = false;
        }
        else if (other.CompareTag("Player2"))
        {
            player2InTrigger = false;
        }
    }
    private void Update()
    {
        if(UIcontroller == null) { 
            UIcontroller = FindObjectOfType<UIController>();
        }
        if (player1InTrigger && player2InTrigger)
        {
            if (GoToLevel == Levels.LevelOne) { SceneManager.LoadScene("LevelOne"); }
            if (GoToLevel == Levels.LevelTwo) 
            {
                UIcontroller.GoToLevel2();
            }
            if (GoToLevel == Levels.LevelThree) { UIcontroller.GoToLevel3(); }
            if (GoToLevel == Levels.LevelFour) { UIcontroller.GoToLevel4(); }
        }
    }
}
