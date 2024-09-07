using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(BoxCollider))]
public class RoomTrigger : AudioPlayer
{
    [Header("RoomTrigger")]
    [SerializeField] private bool onlyTriggerOnce;
    [SerializeField] private UnityEvent onTriggerEvent;

    private bool hasBeenTriggered;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (onlyTriggerOnce && hasBeenTriggered) return;

        PlayAudio();
        onTriggerEvent?.Invoke();

        hasBeenTriggered = true;
    }
}
