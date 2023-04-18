using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] AudioSource MoveSound;
    [Space]
    [SerializeField] Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player1") || col.gameObject.CompareTag("Player2"))
        {
            MoveSound.Play();
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player1") || col.gameObject.CompareTag("Player2"))
        {
            MoveSound.Stop();
        }
    }
}
