using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Camera))]
public class CameraOffsetMover : MonoBehaviour
{
    [SerializeField, Tooltip("The time in seconds, the camera needs to catch up to the target"), Range(0f, 1f)] private float smoothTime = .25f;
    [SerializeField] private Vector3 offset = new(0, 3.5f, -3f);
    [SerializeField, Range(0, 100)] private float fov = 30f;

    private Vector2 velocity;

    private Transform player;
    private Camera cam;

    private void OnValidate()
    {
        GetComponent<Camera>().fieldOfView = fov;
    }

    private void Awake()
    {
        player = PlayerMovement.Instance.transform;
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        cam.fieldOfView = fov;
    }

    private void Update()
    {
        transform.position = new Vector3()
        {
            x = Mathf.SmoothDamp(transform.position.x, player.position.x + offset.x, ref velocity.x, smoothTime),
            y = offset.y,
            z = Mathf.SmoothDamp(transform.position.z, player.position.z + offset.z, ref velocity.y, smoothTime)
        };
    }
}
