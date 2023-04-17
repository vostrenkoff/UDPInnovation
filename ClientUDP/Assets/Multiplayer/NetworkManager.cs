using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Riptide;
using Riptide.Utils;
using Riptide.Transports;
using System;

public enum ClientToServerId : ushort
{
    name = 1,
    input,
}
public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log("Instance already exists >:(");
                Destroy(value);
            }
        }

    }
    public Client client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    [SerializeField] private InputField ipinput;
    [SerializeField] private InputField portinput;
    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        client = new Client();
        //client.Connect($"{ip}:{port}");
        client.Connected += DidConnect;
        client.ConnectionFailed += FailedToConnect;
        client.Disconnected += Disconnected;
        client.ClientDisconnected += PlayerLeft;
    }
    private void FixedUpdate()
    {
        client.Update();
    }
    private void OnApplicationQuit()
    {
        //client.Disconnect();
    }
    public void Connect()
    {
        if (ipinput.text == "" || portinput.text =="")
            client.Connect($"{ip}:{port}");
        else
        {
            client.Connect($"{ipinput.text}:{portinput.text}");
        }

    }
    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.SendName();
    }
    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.BackToMain();
    }
    private void Disconnected(object sender, EventArgs e)
    {
        UIManager.Singleton.BackToMain();
    }
    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        //Destroy(Player.list[e.Id].gameObject);
    }
}