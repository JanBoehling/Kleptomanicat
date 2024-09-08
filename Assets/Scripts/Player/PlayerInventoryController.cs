using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInventoryController : MonoSingleton<PlayerInventoryController>
{
    [SerializeField] private bool[] itemsCollected;

    [SerializeField] private UnityEvent onAllItemsCollectedEvent;

    private int itemCount;
    private TMP_Text itemCountDisplay;

    protected override void Awake()
    {
        base.Awake();
        itemCountDisplay = GameObject.Find("ItemText (TMP)").GetComponent<TMP_Text>();
        if (!itemCountDisplay) Debug.LogWarning($"{GetType()}: Could not fetch item count display text object.");
    }

    private void Start()
    {
        itemCount = itemsCollected.Length;
        itemCountDisplay.text = itemCount.ToString();
    }

    public void CollectItem(int itemID)
    {
        Debug.Log($"{GetType()}: Collecting item {name}.");

        if (itemID > itemsCollected.Length || itemID < 0)
        {
            Debug.LogWarning($"{GetType()}: Given item ID {itemID} did not corrospond to array entry. (itemsCollected.Length = {itemsCollected.Length})");
            return;
        }

        itemsCollected[itemID] = true;

        itemCountDisplay.text = (--itemCount).ToString();

        foreach (var item in itemsCollected) if (!item) return;

        onAllItemsCollectedEvent?.Invoke();
    }
}
