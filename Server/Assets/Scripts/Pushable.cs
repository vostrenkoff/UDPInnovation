using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(Player.isGiantPushing)
                rb.constraints = RigidbodyConstraints2D.None;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
