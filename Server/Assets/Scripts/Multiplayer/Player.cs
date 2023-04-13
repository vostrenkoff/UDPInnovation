using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.VFX;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using Newtonsoft.Json.Bson;

public class Player : MonoBehaviour
{
    public GameObject nicknameText;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float _speed = 8f;
    private float horizontal;
    public float jumpStrength = 160f;
    public  string nicknameOnTop;
    public static Dictionary<ushort, Player> list= new Dictionary<ushort, Player>();
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    private static Camera _camera;


    private IEnumerator coroutine;
    private bool jumpBlock = false;


    private void Move(float command, ushort id)
    {
        Debug.Log("command "+ command);
        if (list.TryGetValue(id, out Player player))
        {
            if(command == 1)
            {
                /*Vector3 addedValue = new Vector3(-10, 0, 0);
                //player.transform.position += addedValue;
                float moveAmount = -10* Time.deltaTime;
                transform.position += new Vector3(moveAmount * _speed, 0f, 0f);*/
                rb.velocity = new Vector2(-_speed, rb.velocity.y);

            }
            if (command == 2)
            {
                /*Vector3 addedValue = new Vector3(10, 0, 0);
                //player.transform.position += addedValue;
                float moveAmount = 10 * Time.deltaTime;
                transform.position += new Vector3(moveAmount * _speed, 0f, 0f);*/

                rb.velocity = new Vector2(_speed, rb.velocity.y);
            }
            if (command == 3 && !jumpBlock)
            {
                Debug.Log("got message 3 ");
                    jumpBlock= true;
                    _playerMovement.Jump();
                    StartCoroutine(coroutine);
            }
            
            /*if (command == 3 && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }*/

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
    private void Start()
    {
        coroutine = Wait();
    }
    private void Update()
    {
        rb.velocity = new Vector2 (horizontal * _speed, rb.velocity.y);

        nicknameText.GetComponent<Text>().text = nicknameOnTop;
        
    }
    private bool isGrounded()
    {
        
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        {
            player.Move(message.GetInt(),fromClientId);
            Debug.Log("Command received: " + message.GetInt());
        }
        else
        {
            Debug.Log("uh oh, couldnt find that player");
            foreach(var item in list) {
                Debug.Log(item + "id: " + message.GetUShort());
            }
        }
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            jumpBlock = false;
        }
    }
}
