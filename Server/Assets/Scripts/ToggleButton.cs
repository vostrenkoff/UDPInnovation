using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] List<GameObject> ActivateObjectList = new List<GameObject>();
    [SerializeField] List<float> YLevelFloat = new List<float>();
    [SerializeField] Vector3 moveUpSpeed;
    [SerializeField] Vector3 moveDownSpeed;
    [SerializeField] float characterHeight;
    float maxHeightWall;
    bool moveDown;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            if (ActivateObjectList[i].transform.position.y >= YLevelFloat[i] && moveDown)
                ActivateObjectList[i].transform.position -= moveDownSpeed * Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        
        if(col.gameObject.CompareTag("Player1"))
        {
            moveDown = false;
            for (int i = 0; i < ActivateObjectList.Count; i++)
            {
                maxHeightWall = YLevelFloat[i] + characterHeight;
                if (maxHeightWall >= ActivateObjectList[i].transform.position.y)
                {
                    ActivateObjectList[i].transform.position += moveUpSpeed * Time.deltaTime;
                }
                else
                {
                    break;
                }
            }
        }
        if (col.gameObject.CompareTag("Player2"))
        {
            Vector3 collisionScale = col.gameObject.transform.localScale;
            if (collisionScale.y >= .1f)
            {
                moveDown = false;
                for (int i = 0; i < ActivateObjectList.Count; i++)
                {
                    maxHeightWall = YLevelFloat[i] + characterHeight;
                    if (maxHeightWall >= ActivateObjectList[i].transform.position.y)
                    {
                        ActivateObjectList[i].transform.position += moveUpSpeed * Time.deltaTime;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        moveDown = true;
    }
}
