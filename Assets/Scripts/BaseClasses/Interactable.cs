using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Interactable : AudioPlayer
{
    [Header("Interactable"), SerializeField, Tooltip("If set, opens preview image instead of collecting it directly.")] private Sprite previewImage;

    protected UnityEvent OnInteractEvent;
    
    private GameObject previewImageCanvas;

    protected override void Awake()
    {
        base.Awake();
        previewImageCanvas = GameObject.Find("Preview Image Canvas"); // ToDo: replace "find"
    }

    public virtual void Interact()
    {
        Debug.Log($"Interacting with {name}");

        ShowPreviewImage();

        PlayAudio();

        OnInteractEvent?.Invoke();
    }

    private void ShowPreviewImage()
    {
        // If preview image is set, shows preview image canvas object
        if (!previewImage || !previewImageCanvas) return;

        previewImageCanvas.transform.GetComponentInChildren<SpriteRenderer>().sprite = previewImage; // ToDo: replace "GetComponentInChildren"
        previewImageCanvas.SetActive(true);
    }
}
