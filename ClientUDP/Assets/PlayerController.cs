using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float dirX;
    float moveSpeed = 20f;
    int command = 0;
    public void goLeft()
    {
        command = 1;
        SendInput();
    }
    public void goRight()
    {
        command = 2;
        SendInput();
    }
    public void jump()
    {
        command = 3;
        SendInput();
    }
    public void restart()
    {
        command = 4;
        SendInput();
    }
    public void ability()
    {
        command = 5;
        SendInput();
    }
    public void reset()
    {
        command = 0;
        SendInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //dirX = Input.acceleration.x * moveSpeed;
        //Debug.Log(dirX);
        SendInput();
        //command= 0;
    }
    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.input);
        message.AddInt(command);
        NetworkManager.Singleton.client.Send(message);
    }
}
