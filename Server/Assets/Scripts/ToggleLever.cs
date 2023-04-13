using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLever : MonoBehaviour
{
    [SerializeField] List<GameObject> ActivateObjectList = new List<GameObject>();
    [SerializeField] List<float> YLevelFloat = new List<float>();
    [SerializeField] Vector3 moveUpSpeed;
    [SerializeField] Vector3 moveDownSpeed;
    float maxHeightWall;
    [SerializeField] float characterHeight;
    [SerializeField] bool isTurned = false;

    void Update()
    {
        MoveDown();
        MoveUp();
    }
    void MoveUp()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            maxHeightWall = YLevelFloat[i] + characterHeight;
            if (maxHeightWall > ActivateObjectList[i].transform.position.y && isTurned)
            {
                ActivateObjectList[i].transform.position += moveUpSpeed * Time.deltaTime;
            }
        }
    }
    void MoveDown()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            if (ActivateObjectList[i].transform.position.y >= YLevelFloat[i] && !isTurned)
                ActivateObjectList[i].transform.position -= moveDownSpeed * Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isTurned)
        {
            isTurned = true;
        }
        else
        {
            isTurned = false;
        }
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z + 90);
    }
}
