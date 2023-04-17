using UnityEngine;

public class NEWMultipleTargetCamera : MonoBehaviour
{
    public Transform[] targets;          // An array of the targets to follow
    public float smoothTime = 0.5f;      // The smoothing time for camera movement
    public Vector3 offset;              // The offset from the center of the targets
    public float minZoom = 1f;           // The minimum zoom level
    public float maxZoom = 20f;          // The maximum zoom level
    public float zoomLimiter = 50f;      // The zoom limit for the camera

    private Camera cam;                 // The camera component
    private Vector3 velocity;           // The current velocity for camera movement

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Length == 0)
            return;

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
            return targets[0].position;

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
}