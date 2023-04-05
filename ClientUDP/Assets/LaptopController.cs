using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRb;
    [SerializeField]
    private float playerSpeed = 10f;

    private void Update()
    {
        PlayerMoveLeft();
        PlayerMoveRight();
    }

    void PlayerMoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRb.velocity = Vector2.left * playerSpeed;
        } 
    }

    void PlayerMoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            playerRb.velocity = Vector2.right * playerSpeed;
        }
    }
}
