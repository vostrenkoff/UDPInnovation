using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float dirX;
    float moveSpeed = 20f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = Input.acceleration.x * moveSpeed;
        Debug.Log(dirX);
        SendInput();
    }
    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.input);
        message.AddFloat(dirX);
        NetworkManager.Singleton.client.Send(message);
    }
}
