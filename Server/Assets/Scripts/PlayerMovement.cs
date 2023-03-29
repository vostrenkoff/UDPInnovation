using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CharacterController controller;
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

   private void Move()
    {
        //controller.Move()
    }
}
