using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] List<GameObject> Slides;
    int i = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && i < Slides.Count)
        {
            Slides[i].SetActive(false);
            i++;
            Slides[i].SetActive(true);
        }
        if(i >= 5)
        {
            SceneManager.LoadScene("LevelOne");
        }
    }
}
