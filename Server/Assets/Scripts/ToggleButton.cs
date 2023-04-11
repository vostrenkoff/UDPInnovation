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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            Debug.Log(ActivateObjectList[i].transform.position.y);
            Debug.Log(maxHeightWall);
            if (ActivateObjectList[i].transform.position.y >= YLevelFloat[i] && moveDown)
                ActivateObjectList[i].transform.position -= moveDownSpeed * Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        moveDown = false;
        if(col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2")
        {
            for (int i = 0; i < ActivateObjectList.Count; i++)
            {
                maxHeightWall = YLevelFloat[i] + characterHeight;
                if (maxHeightWall >= ActivateObjectList[i].transform.position.y)
                {
                    Debug.Log("trigger");
                    ActivateObjectList[i].transform.position += moveUpSpeed * Time.deltaTime;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        moveDown = true;
    }
}
