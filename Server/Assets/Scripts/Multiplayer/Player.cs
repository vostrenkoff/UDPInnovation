using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.VFX;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField] public Character characterType;

    private IEnumerator coroutine;
    private bool jumpBlock = false;
    
    public enum Character
    {
        Giant,
        Shrink
    }
    
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
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (command == 2)
            {
                /*Vector3 addedValue = new Vector3(10, 0, 0);
                //player.transform.position += addedValue;
                float moveAmount = 10 * Time.deltaTime;
                transform.position += new Vector3(moveAmount * _speed, 0f, 0f);*/
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                rb.velocity = new Vector2(_speed, rb.velocity.y);
            }
            if (command == 3 && !jumpBlock)
            {
                Debug.Log("got message 3 ");
                    jumpBlock= true;
                    _playerMovement.Jump();
                    StartCoroutine(coroutine);
            }
            if(command == 5)
            {
                if(characterType == Character.Giant)
                {
                    Debug.Log("Giant pushes.");

                }
                if(characterType == Character.Shrink)
                {
                    Debug.Log("Shrinker shrinks.");
                }
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
        Scene currentScene = SceneManager.GetActiveScene();
        Vector3 spawnPos = new Vector3(0, 0, 0);

        GameObject TextObject1 = GameObject.Find("P1GameObject");
        GameObject TextObject2 = GameObject.Find("P2GameObject");
        Text myText1 = TextObject1.GetComponent<Text>();
        Text myText2 = TextObject2.GetComponent<Text>();

        if (currentScene.name == "Main") { spawnPos = new Vector3(0, 0, 0); }
        else if (currentScene.name == "LevelOne") { spawnPos = new Vector3(-661, -150, 0); }
        else if (currentScene.name == "LevelTwo") { spawnPos = new Vector3(0, 0, 0); }
        else if (currentScene.name == "LevelThree") { spawnPos = new Vector3(0, 0, 0); }
        else if (currentScene.name == "LevelFour") { spawnPos = new Vector3(0, 0, 0); }
        if (list.Count == 1)
        {
            
            
            myText1.text = username + " connected";

            Player player = Instantiate(GameLogic.Singleton.Player1Prefab, new Vector3(202, 404, 0f), Quaternion.identity).GetComponent<Player>();
            GameObject canvas = GameObject.Find("Canvas");
            player.transform.SetParent(canvas.transform);
            player.transform.localPosition = spawnPos;
            player.Id = id;
            player.name = username;
            list.Add(id, player);
            player.nicknameOnTop = username;
            _camera = FindObjectOfType<Camera>();
            _camera.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        }
        if (list.Count == 0)
        {
            
            
            myText2.text = username + " connected";

            Player player = Instantiate(GameLogic.Singleton.Player2Prefab, new Vector3(303f, 404f, 0f), Quaternion.identity).GetComponent<Player>();
            GameObject canvas = GameObject.Find("Canvas");
            player.transform.SetParent(canvas.transform);
            player.transform.localPosition = spawnPos;
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
