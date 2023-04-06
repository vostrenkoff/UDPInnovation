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
    public GameObject groundCheck;
    private float _speed = 250f;
    public float jumpforce = 20f;
    public  string nicknameOnTop;
    public static Dictionary<ushort, Player> list= new Dictionary<ushort, Player>();
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    private static Camera _camera;
    private bool _isGrounded = false;
    private void Move(float dir, ushort id)
    {
        //Debug.Log("move "+dir);
        if (list.TryGetValue(id, out Player player))
        {
            Vector2 movement = new Vector2(dir * _speed, 0f);

            // Set the velocity of the Rigidbody2D component to the movement vector
            player.GetComponent<Rigidbody2D>().velocity = movement;
        }
    }
    private void Jump(ushort id)
    {
        if (list.TryGetValue(id, out Player player))
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null && GroundCheck.isGrounded)
            {
                
                rb.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Jump is not allowed.");
            }
        }
        else 
            Debug.Log("ERROR");
    }
    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }
    public static void Spawn(ushort id, string username)
    {
        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab, new Vector3(0f,0f,0f), Quaternion.identity).GetComponent<Player>();
        GameObject canvas = GameObject.Find("Canvas");
        player.transform.SetParent(canvas.transform);
        player.transform.localPosition= Vector3.zero;
        player.Id = id;
        player.name = username;
        list.Add(id, player);
        player.nicknameOnTop = username;
        _camera = FindObjectOfType<Camera>();
        _camera.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
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
        //Debug.Log(message.GetInt());
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
    [MessageHandler((ushort)ClientToServerId.jumpinput)]
    private static void JumpInput(ushort fromClientId, Message message)
    {
        //
        if (list.TryGetValue(fromClientId, out Player player))
        {
            Debug.Log("jummmped");
            player.Jump(fromClientId);
        }
        //player.Move(message.GetFloat(), fromClientId);

        else
        {
            Debug.Log("uh oh, couldnt find that player");
            foreach (var item in list)
            {
                Debug.Log(item + "id: " + message.GetUShort());
            }
        }
        Debug.Log(message.GetInt());
    }
    private bool IsGrounded()
    {
        float extraHeightText = .1f;
        
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, extraHeightText, Physics2D.GetLayerCollisionMask(gameObject.layer));
        if(raycastHit.collider== null)
        {
            Debug.Log(" not COLLIDEd ");
        }
        else
            Debug.Log(" COLLIDEd ");

        return raycastHit.collider != null;

    }
}
