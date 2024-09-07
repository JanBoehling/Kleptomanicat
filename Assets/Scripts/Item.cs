using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : Interactable
{
    [Header("Item"), SerializeField, Tooltip("If set, opens preview image instead of collecting it directly.")] private Sprite previewImage;
    [SerializeField] private int itemID = -1;
    [SerializeField] private string itemName = "Item";
    [SerializeField, Range(0, 100000000)] private uint itemValue;
    [SerializeField] private UnityEvent onItemTakeEvent;

    private Canvas itemPreviewCanvas;
    private Image itemPreviewImage;
    protected Button itemTakeButton;
    private TextMeshProUGUI itemPreviewTmp;

    protected override void Awake()
    {
        base.Awake();

        itemPreviewCanvas = GameObject.Find("ItemPreviewCanvas").GetComponent<Canvas>();
        if (itemPreviewCanvas)
        {
            itemPreviewImage = itemPreviewCanvas.transform.GetChild(0).Find("ItemPreviewImage").GetComponent<Image>();
            itemPreviewTmp = itemPreviewCanvas.transform.GetChild(0).Find("ItemPreviewTmp").GetComponent<TextMeshProUGUI>();
            itemTakeButton = itemPreviewCanvas.transform.GetChild(0).Find("TakeButton").GetComponent<Button>();
        }
    }

    protected virtual void Start()
    {
        OnInteractEvent?.AddListener(ShowPreviewImage);
    }

    private void ShowPreviewImage()
    {
        // If preview image is set, shows preview image canvas object
        if (!previewImage || !itemPreviewCanvas) return;

        itemPreviewImage.sprite = previewImage;
        itemPreviewTmp.text = itemName;

        itemTakeButton.onClick.AddListener(OnItemInteraction);

        PlayerMovement.Instance.GetComponent<PlayerInput>().currentActionMap.Disable();

        itemPreviewCanvas.enabled = true;
    }

    protected virtual void OnItemInteraction()
    {
        onItemTakeEvent?.Invoke();

        if (itemID > -1) PlayerInventoryController.Instance.CollectItem(itemID);

        itemPreviewCanvas.enabled = false;

        PlayerMoneyController.Instance.AddMoney(itemValue);

        Destroy(gameObject);

        PlayerMovement.Instance.GetComponent<PlayerInput>().currentActionMap.Enable();

        itemTakeButton.onClick.RemoveAllListeners();
    }
}
