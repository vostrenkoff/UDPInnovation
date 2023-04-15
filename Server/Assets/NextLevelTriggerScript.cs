using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Player;

public class NextLevelTriggerScript : MonoBehaviour
{
    public bool player1InTrigger;
    public bool player2InTrigger;
    [SerializeField] public Levels GoToLevel;
    public enum Levels
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour
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
        if(player1InTrigger && player2InTrigger)
        {
            if(GoToLevel == Levels.LevelOne) { SceneManager.LoadScene("LevelOne"); }
            if(GoToLevel == Levels.LevelTwo) { SceneManager.LoadScene("LevelTwo"); }
            if(GoToLevel == Levels.LevelThree) { SceneManager.LoadScene("LevelThree"); }
            if(GoToLevel == Levels.LevelFour) { SceneManager.LoadScene("LevelFour"); }
        }
    }
}
