using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] List<GameObject> ActivateObjectList = new List<GameObject>();
    [SerializeField] Vector3 moveUpSpeed;
    [SerializeField] Vector3 moveDownSpeed;
    [SerializeField] int maxHeightWall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            if(ActivateObjectList[i].transform.position.y > -1.5f)
                ActivateObjectList[i].transform.position -= moveDownSpeed * Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if(col.gameObject.name == "Player")
        {
            for (int i = 0; i < ActivateObjectList.Count; i++)
            {
                if(maxHeightWall > ActivateObjectList[i].transform.position.y)
                    ActivateObjectList[i].transform.position += moveUpSpeed * Time.deltaTime;
            }
        }
    }
}
