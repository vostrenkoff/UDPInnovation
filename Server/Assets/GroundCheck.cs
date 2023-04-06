using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("Collision detected with " + collision.gameObject.name);
            isGrounded= true;
      
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log("No Collision");
    }
}
