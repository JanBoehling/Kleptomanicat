using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Transform _teleportToPoint;

    private CameraManager _cameraManager;

    private void Awake()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        _teleportToPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerMovement>())
            return;

        _cameraManager.SetCameraActive(_camera);
        PlayerMovement.Instance.transform.position = _teleportToPoint.position;
    }
}
