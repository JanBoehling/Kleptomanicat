using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Camera))]
public class CameraOffsetMover : MonoBehaviour
{
    [SerializeField, Tooltip("The time in seconds, the camera needs to catch up to the target"), Range(0f, 1f)] private float smoothTime = .25f;
    private Vector2 velocity;

    private Transform player;

    private void Awake()
    {
        player = PlayerMovement.Instance.transform;
    }

    private void Start()
    {
        GetComponent<Camera>().fieldOfView = 30f;

        var pos = transform.position;
        pos.y = 3.5f;
        transform.position = pos;
    }

    private void Update()
    {
        transform.position = new Vector3()
        {
            x = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTime),
            y = 3.5f,
            z = Mathf.SmoothDamp(transform.position.z, player.position.z - 3, ref velocity.y, smoothTime)
        };
    }
}
