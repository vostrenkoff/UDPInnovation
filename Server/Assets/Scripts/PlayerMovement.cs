using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpStrength;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private void OnValidate()
    {
        if(player == null)
        {
            player = GetComponent<Player>();
        }
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

   private void Update()
    {
        Debug.Log("update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("yes i jumped");
            Jump();
        }
    }
    public void Jump()
    {
        if(isGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }
    private bool isGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
