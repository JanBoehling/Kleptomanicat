using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInventoryController : MonoSingleton<PlayerInventoryController>
{
    [SerializeField] private bool[] itemsCollected;

    [SerializeField] private UnityEvent onAllItemsCollectedEvent;

    public void CollectItem(int itemID)
    {
        Debug.Log($"{GetType()}: Collecting item {name}.");

        if (itemID > itemsCollected.Length || itemID < 0)
        {
            Debug.LogWarning($"{GetType()}: Given item ID {itemID} did not corrospond to array entry. (itemsCollected.Length = {itemsCollected.Length})");
            return;
        }

        itemsCollected[itemID] = true;

        foreach (var item in itemsCollected) if (!item) return;

        onAllItemsCollectedEvent?.Invoke();
    }
}
