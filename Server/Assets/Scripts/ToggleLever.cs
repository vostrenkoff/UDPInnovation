using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLever : MonoBehaviour
{
    [SerializeField] Sprite LeverOff;
    [SerializeField] Sprite LeverOn;
    private SpriteRenderer spriteRenderer;
    [Space]
    [SerializeField] List<GameObject> ActivateObjectList = new List<GameObject>();
    [SerializeField] List<float> YLevelFloat = new List<float>();
    [SerializeField] Vector3 moveUpSpeed;
    [SerializeField] Vector3 moveDownSpeed;
    float maxHeightWall;
    [SerializeField] float characterHeight;
    [SerializeField] bool isTurned = false;
    [SerializeField] ScriptableObject SGCharacter;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = LeverOff;
    }
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
        if (col.gameObject.CompareTag("Player2"))
        {
            Vector3 collisionScale = col.gameObject.transform.localScale;
            if(collisionScale.y >= .1f)
            {
                if (!isTurned)
                {
                    isTurned = true;
                    spriteRenderer.sprite = LeverOn;
                }
                else
                {
                    spriteRenderer.sprite = LeverOff;
                    isTurned = false;
                }
            }
        }
        if (col.gameObject.CompareTag("Player1"))
        {
            if (!isTurned)
            {
                isTurned = true;
                spriteRenderer.sprite = LeverOn;
            }
            else
            {
                spriteRenderer.sprite = LeverOff;
                isTurned = false;
            }
        }
    }
}
