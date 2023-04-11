using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.VFX;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    public GameObject nicknameText;
    private float _speed = 2.5f;
    public  string nicknameOnTop;
    public static Dictionary<ushort, Player> list= new Dictionary<ushort, Player>();
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    private static Camera _camera;
    private void Move(float dir, ushort id)
    {
        Debug.Log("move "+dir);
        if (list.TryGetValue(id, out Player player))
        {
            Vector3 addedValue = new Vector3(dir, 0, 0);
            //player.transform.position += addedValue;
            float moveAmount = dir * Time.deltaTime;
            
            transform.position += new Vector3(moveAmount*_speed, 0f, 0f);
        }
    }

    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }
    public static void Spawn(ushort id, string username)
    {
        if(list.Count == 1)
        {
            Player player = Instantiate(GameLogic.Singleton.Player1Prefab, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Player>();
            GameObject canvas = GameObject.Find("Canvas");
            player.transform.SetParent(canvas.transform);
            player.transform.localPosition = Vector3.zero;
            player.Id = id;
            player.name = username;
            list.Add(id, player);
            player.nicknameOnTop = username;
            _camera = FindObjectOfType<Camera>();
            _camera.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        }
        if (list.Count == 0)
        {
            Player player = Instantiate(GameLogic.Singleton.Player2Prefab, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Player>();
            GameObject canvas = GameObject.Find("Canvas");
            player.transform.SetParent(canvas.transform);
            player.transform.localPosition = Vector3.zero;
            player.Id = id;
            player.name = username;
            list.Add(id, player);
            player.nicknameOnTop = username;
            _camera = FindObjectOfType<Camera>();
            _camera.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        }

    }
    private void Update()
    {
        nicknameText.GetComponent<Text>().text = nicknameOnTop;
    }
    private void OnDestroy()
    {
        list.Remove(Id);
        _camera.GetComponent<MultipleTargetCamera>().targets.Remove(transform);
    }
    [MessageHandler((ushort)ClientToServerId.input)]
    private static void Input(ushort fromClientId, Message message)
    {
        //Debug.Log(message.GetFloat());
        if (list.TryGetValue(fromClientId, out Player player))
            player.Move(message.GetFloat(),fromClientId);
        else
        {
            Debug.Log("uh oh, couldnt find that player");
            foreach(var item in list) {
                Debug.Log(item + "id: " + message.GetUShort());
            }
        }
    }
}
