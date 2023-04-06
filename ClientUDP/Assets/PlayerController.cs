using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float dirX;
    float moveSpeed = 20f;

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    bool isLeft = false;
    bool isRight = false;
    public void MoveLeft()
    {
        isLeft = true;
        SendInput();
    }
    public void MoveRight()
    {
        isRight = true;
        SendInput();
    }
    public void Jump()
    {
        SendJumpCommand();
    }
    public void StopMoving()
    {
        isLeft = false;
        isRight = false;
    }
    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = Input.acceleration.x * moveSpeed;
        Debug.Log(dirX);
        SendInput();
        
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            // Perform your "shaking actions" here. If necessary, add suitable
            // guards in the if check above to avoid redundant handling during
            // the same shake (e.g. a minimum refractory period).
            Debug.Log("Shake event detected at time " + Time.time);
            
        }
    }

    private void SendInput()
    {
        float input = 0;
        if(isLeft)
        {
            input = -1.0f;
        }
        if(isRight)
        {
            input = 1.0f;
        }
        
        Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.input);
        message.AddFloat(input);
        NetworkManager.Singleton.client.Send(message);
    }
    private void SendJumpCommand()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, ClientToServerId.jumpinput);
        message.AddInt(1);
        NetworkManager.Singleton.client.Send(message);
    }
}
