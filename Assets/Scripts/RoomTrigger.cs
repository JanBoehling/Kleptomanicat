using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomTrigger : AudioPlayer
{
    [SerializeField] private bool onlyTriggerOnce;

    private bool hasBeenTriggered;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onlyTriggerOnce && hasBeenTriggered) return;

        PlayAudio();
        hasBeenTriggered = true;
    }
}
