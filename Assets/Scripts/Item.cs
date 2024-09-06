using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : Interactable
{
    [Header("Item"), SerializeField, Tooltip("If set, opens preview image instead of collecting it directly.")] private Sprite previewImage;
    [SerializeField] private int itemID = -1;

    [Header("")]
    [SerializeField] private GameObject itemPreviewCanvas;
    [SerializeField] private Image itemPreviewImage;
    [SerializeField] private Button itemTakeButton;

    private void Start()
    {
        OnInteractEvent?.AddListener(ShowPreviewImage);
    }

    private void ShowPreviewImage()
    {
        // If preview image is set, shows preview image canvas object
        if (!previewImage || !itemPreviewCanvas) return;

        itemPreviewImage.sprite = previewImage;

        itemTakeButton.onClick.AddListener(() => PlayerInventoryController.Instance.CollectItem(itemID));
        itemTakeButton.onClick.AddListener(() => itemPreviewCanvas.SetActive(false));
        itemTakeButton.onClick.AddListener(() => Destroy(gameObject));
        itemTakeButton.onClick.AddListener(PlayerMovement.Instance.GetComponent<PlayerInput>().currentActionMap.Enable);
        itemTakeButton.onClick.AddListener(itemTakeButton.onClick.RemoveAllListeners);

        PlayerMovement.Instance.GetComponent<PlayerInput>().currentActionMap.Disable();

        itemPreviewCanvas.SetActive(true);
    }
}
