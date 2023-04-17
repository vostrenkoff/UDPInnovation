using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip PressButton;
    [SerializeField] AudioClip MoveObject;
    [Space]
    [SerializeField] public Sprite ButtonOff;
    [SerializeField] public Sprite ButtonOn;
    public SpriteRenderer spriteRenderer;
    [Space]
    [SerializeField] List<GameObject> ActivateObjectList = new List<GameObject>();
    [SerializeField] List<float> YLevelFloat = new List<float>();
    [SerializeField] Vector3 moveUpSpeed;
    [SerializeField] Vector3 moveDownSpeed;
    [SerializeField] float characterHeight;
    float maxHeightWall;
    bool moveDown;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ButtonOff;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ActivateObjectList.Count; i++)
        {
            if (ActivateObjectList[i].transform.position.y >= YLevelFloat[i] && moveDown)
                ActivateObjectList[i].transform.position -= moveDownSpeed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player2"))
        {
            Vector3 collisionScale = col.gameObject.transform.localScale;
            if (collisionScale.y >= .1f)
            {
                Source.PlayOneShot(PressButton);
                Source.PlayOneShot(MoveObject);
            }
        }
        if (col.gameObject.CompareTag("Player1"))
        {
            Source.PlayOneShot(PressButton);
            Source.PlayOneShot(MoveObject);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        spriteRenderer.sprite = ButtonOn;
        if (col.gameObject.CompareTag("Player1"))
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
        Source.PlayOneShot(PressButton);
        moveDown = true;
        spriteRenderer.sprite = ButtonOff;
    }
}
