using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera[] _allCameras;
    [SerializeField] private Camera _activeCamera;

    private void Awake()
    {
        _allCameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (Camera camera in _allCameras)
        {
            camera.enabled = false;
        }
        _activeCamera.enabled = true;
    }

    public void SetCameraActive(Camera nCamera)
    {
        _activeCamera.enabled = false;
        _activeCamera = nCamera;
        _activeCamera.enabled = true;
    }
}
