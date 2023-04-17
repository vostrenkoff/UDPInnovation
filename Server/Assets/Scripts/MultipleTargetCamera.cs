using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomDivider = 50f;
    public Vector3 velocity;
    private Camera cam;


    private float minX = -350f;
    private float maxX = 1660f;
    private float minY = 200f;
    private float maxY = 1000f;

    public GameObject background;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;
    public GameObject background5;
    public GameObject background6;
    public GameObject background7;
    public GameObject background8;
    public GameObject background9;

    public int background1Speed;
    public int background2Speed;
    public int background3Speed;
    public int background4Speed;
    public int background5Speed;
    public int background6Speed;
    public int background7Speed;
    public int background8Speed;
    public int background9Speed;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        background.transform.localPosition = new Vector3(transform.position.x * (background1Speed * 0.00001f), 0f, 0f);
        background2.transform.localPosition = new Vector3(transform.position.x * (background2Speed * 0.00001f), 0f, 0f);
        background3.transform.localPosition = new Vector3(transform.position.x * (background3Speed * 0.00001f), 0f, 0f);
        background4.transform.localPosition = new Vector3(transform.position.x * (background4Speed * 0.00001f), 0f, 0f);
        background5.transform.localPosition = new Vector3(transform.position.x * (background5Speed * 0.00001f), 0f, 0f);
        background6.transform.localPosition = new Vector3(transform.position.x * (background6Speed * 0.00001f), 0f, 0f);
        background7.transform.localPosition = new Vector3(transform.position.x * (background7Speed * 0.00001f), 0f, 0f);
        background8.transform.localPosition = new Vector3(transform.position.x * (background8Speed * 0.00001f), 0f, 0f);
        background9.transform.localPosition = new Vector3(transform.position.x * (background9Speed * 0.00001f), 0f, 0f);



        if (cam.orthographicSize < 300)
        {
            //minY = 200;
        }
        else
        {
            minY= 200;
        }
        if (targets.Count > 0)
        {
            Move();
            Zoom();
        }
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player1 != null)
        {
            if (!targets.Contains(player1.transform))
                targets.Add(player1.transform);
        }
        if (player2 != null)
        {
            if (!targets.Contains(player2.transform))
                targets.Add(player2.transform);
        }
    }

    private void Zoom()
    {
        float distance = GetGreatestDistance();
        //float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomDivider);
        cam.orthographicSize = Mathf.Clamp(distance,200,430);
    }

    /*float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }*/
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        // Calculate the distance based on the largest x, y, or z component of the Vector3 distance between the targets
        float distanceX = bounds.max.x - bounds.min.x;
        float distanceY = bounds.max.y - bounds.min.y;
        float distanceZ = bounds.max.z - bounds.min.z;
        float distance = Mathf.Max(distanceX, distanceY, distanceZ);

        return distance;
    }
    /*void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }*/
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
