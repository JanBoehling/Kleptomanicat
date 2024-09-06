using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(SpriteRenderer))]
public abstract class Interactable : AudioPlayer
{
    [Header("Interactable"), SerializeField, Tooltip("If set, opens preview image instead of collecting it directly.")] private Sprite previewImage;

    protected UnityEvent OnInteractEvent;
    
    private GameObject itemPreviewCanvas;
    private Image itemPreviewImage;
    private Button itemTakeButton;

    protected override void Awake()
    {
        base.Awake();
        itemPreviewCanvas = GameObject.Find("ItemPreviewCanvas");
        if (itemPreviewCanvas) itemPreviewImage = itemPreviewCanvas.transform.Find("ItemPreviewImage").GetComponent<Image>();
        if (itemPreviewCanvas) itemTakeButton = itemPreviewCanvas.transform.Find("TakeButton").GetComponent<Button>();
    }

    public virtual void Interact()
    {
        Debug.Log($"{GetType()}: Interacting with {name}");

        ShowPreviewImage();

        PlayAudio();

        OnInteractEvent?.Invoke();
    }

    private void ShowPreviewImage()
    {
        // If preview image is set, shows preview image canvas object
        if (!previewImage || !itemPreviewCanvas) return;

        itemPreviewImage.sprite = previewImage;
        itemTakeButton.onClick.AddListener(null); // ToDo: add call to inventory system
        itemTakeButton.onClick.AddListener(itemTakeButton.onClick.RemoveAllListeners);
        itemPreviewCanvas.SetActive(true);
    }
}
