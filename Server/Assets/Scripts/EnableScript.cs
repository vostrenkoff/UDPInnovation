using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour
{
    [SerializeField] List<ToggleButton> buttonScript;
    [SerializeField] ToggleLever leverScript;

    // Start is called before the first frame update
    void Start()
    {
        if (leverScript != null)
        {
            leverScript.enabled = false;
        }
    }
    private void Update()
    {
        if (!leverScript.isTurned)
        {
            leverScript.spriteRenderer.sprite = leverScript.LeverOff;
        }
        else
        {
            leverScript.spriteRenderer.sprite = leverScript.LeverOn;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        for (int i = 0; i < buttonScript.Count; i++)
        {
            if (buttonScript != null)
            {
                buttonScript[i].enabled = false;
            }
        }
        if (leverScript != null)
        {
            leverScript.enabled = true;
        }
    }
}
