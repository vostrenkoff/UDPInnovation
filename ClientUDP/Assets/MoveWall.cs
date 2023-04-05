using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    Vector3 startPos;
    float maxYPos;
    [SerializeField] Vector3 moveUpSpeed;
    public bool Activate = false;

    BoxCollider2D m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        startPos.y = transform.position.y;
        maxYPos = startPos.y + m_Collider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            System.Console.WriteLine("Is active");
            if (transform.position.y < maxYPos)
            {
                transform.position += moveUpSpeed;
            }
            else
            {
                System.Console.WriteLine("maxHeight Reached");
            }
        }
    }
}
