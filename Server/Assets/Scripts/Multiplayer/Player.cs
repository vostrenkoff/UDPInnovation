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
    [SerializeField] private Animator AnimController;
    private float horizontal;
    public float jumpStrength = 160f;
    public GameObject startYes;
    public  string nicknameOnTop;
    public static Dictionary<ushort, Player> list= new Dictionary<ushort, Player>();
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    private static Camera _camera;
    [SerializeField] public Character characterType;

    static UIController UIcontroller;

    private IEnumerator coroutine;
    private bool jumpBlock = false;
    private bool shrinked;
    private bool startedLevel1 = false;
    [Space]

    [SerializeField] Vector3 bigSize = new Vector3(0.22f, 0.22f, 0.22f);
    [SerializeField] Vector3 smallSize = new Vector3(0.09f, 0.09f, 0.09f);
    bool bigSizeActive = false;

    public enum Character
    {
        Giant,
        Shrink
    }
    
    private void Move(float command, ushort id)
    {
        Debug.Log(rb.velocity.y);
        if(rb.velocity.y > 1f)
        {
            AnimController.SetBool("isJumping", true);
        }
        else if(rb.velocity.y <-10)
        {
            AnimController.SetBool("isJumping", false);
            AnimController.SetBool("isFalling", true);
        }
        else if (rb.velocity.y <1 && rb.velocity.y>=0)
        {
            AnimController.SetBool("isJumping", false);
            AnimController.SetBool("isFalling", false);
        }
        if (list.TryGetValue(id, out Player player))
        {
            if (command == 0)
            {
                AnimController.SetBool("isWalking", false);
            }
            if (command == 1)
            {
                /*Vector3 addedValue = new Vector3(-10, 0, 0);
                //player.transform.position += addedValue;
                float moveAmount = -10* Time.deltaTime;
                transform.position += new Vector3(moveAmount * _speed, 0f, 0f);*/
                rb.velocity = new Vector2(-_speed, rb.velocity.y);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                AnimController.SetBool("isWalking", true);
            }
            if (command == 2)
            {
                /*Vector3 addedValue = new Vector3(10, 0, 0);
                //player.transform.position += addedValue;
                float moveAmount = 10 * Time.deltaTime;
                transform.position += new Vector3(moveAmount * _speed, 0f, 0f);*/
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                rb.velocity = new Vector2(_speed, rb.velocity.y);
                AnimController.SetBool("isWalking", true);
            }
            if (command == 3 && !jumpBlock)
            {
                //AnimController.SetTrigger("jump");
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
                    AnimController.SetBool("isAbility", true);

                }
                if(characterType == Character.Shrink)
                {
                    Debug.Log("Shrinker shrinks.");
                    if (!shrinked)
                    {
                        transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                        shrinked = true;
                    }
                    else
                    {
                        transform.localScale = new Vector3(1f, 1f, 1);
                        shrinked = false;
                    }
                }
            }
            if (command == 6)
            {
                if (characterType == Character.Giant)
                {
                    AnimController.SetBool("isAbility", false);
                }
            }
            if(command == 7)
            {
                if (GameObject.Find("LevelOne") != null)
                {
                    Player[] players = FindObjectsOfType<Player>();

                    float startpos = 0;
                    foreach (Player playerr in players)
                    {

                        playerr.transform.localPosition = new Vector3(-700 + startpos, -421f, 0f);
                        startpos += 100;
                    }
                }
                else if (GameObject.Find("LevelTwo") != null)
                {
                    Player[] players = FindObjectsOfType<Player>();

                    float startpos = 0;
                    foreach (Player playerr in players)
                    {

                        playerr.transform.localPosition = new Vector3(-35 + startpos, -421f, 0f);
                        startpos += 100;
                    }
                }
                else if (GameObject.Find("LevelThree") != null)
                {
                    Player[] players = FindObjectsOfType<Player>();

                    float startpos = 0;
                    foreach (Player playerr in players)
                    {

                        playerr.transform.localPosition = new Vector3(-812 + startpos, 158f, 0f);
                        startpos += 100;
                    }
                }
                else if (GameObject.Find("LevelFour") != null)
                {
                    Player[] players = FindObjectsOfType<Player>();

                    float startpos = 0;
                    foreach (Player playerr in players)
                    {

                        player.transform.localPosition = new Vector3(-805 + startpos, 320f, 0f);
                        startpos += 100;
                    }
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

        UIcontroller = FindObjectOfType<UIController>();
        Scene currentScene = SceneManager.GetActiveScene();
        Vector3 spawnPos = new Vector3(0, 0, 0);

        string sceneName = currentScene.name;
        GameObject TextObject1;
        GameObject TextObject2;
        Text myText1;
        Text myText2;




        if (sceneName == "Main")
        {
            TextObject1 = GameObject.Find("P1GameObject");
            TextObject2 = GameObject.Find("P2GameObject");
            myText1 = TextObject1.GetComponent<Text>();
            myText2 = TextObject2.GetComponent<Text>();
        }

        if (currentScene.name == "Main") { spawnPos = new Vector3(11000, 0, 0); }
        else if (currentScene.name == "LevelOne") { spawnPos = new Vector3(-661, -150, 0); }
        else if (currentScene.name == "LevelTwo") { spawnPos = new Vector3(0, 0, 0); }
        else if (currentScene.name == "LevelThree") { spawnPos = new Vector3(0, 0, 0); }
        else if (currentScene.name == "LevelFour") { spawnPos = new Vector3(0, 0, 0); }

        if (list.Count == 1)
        {
            
            if (sceneName == "Main")
            {
                if (UIcontroller != null)
                {
                    UIcontroller.StartPlayScreen();
                    UIcontroller.BlueConnected();
                }
                TextObject1 = GameObject.Find("P1GameObject");
                myText1 = TextObject1.GetComponent<Text>();
                myText1.text = username;
            }
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
            if (sceneName == "Main")
            {
                if (UIcontroller != null)
                {
                    
                    UIcontroller.StartPlayScreen();
                    UIcontroller.RedConnected();
                }
                TextObject2 = GameObject.Find("P2GameObject");
                myText2 = TextObject2.GetComponent<Text>();
                myText2.text = username;
            }

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
        
        if(list.Count == 2 && !startedLevel1)
        {
            UIcontroller.Ready();
            startedLevel1 = true;
        }
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
            yield return new WaitForSeconds(1f);
            jumpBlock = false;
        }
    }
}
