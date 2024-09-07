using System;
using UnityEngine;
using UnityEngine.Events;

public class Key : Item
{
    [SerializeField, Header("Key")] private UnityEvent onKeyPickupEvent;

    protected override void Start()
    {
        base.Start();

        OnInteractEvent?.AddListener(() => itemTakeButton.onClick.AddListener(onKeyPickupEvent.Invoke));
    }

    protected override void OnItemInteraction()
    {
        base.OnItemInteraction();
    }
}
