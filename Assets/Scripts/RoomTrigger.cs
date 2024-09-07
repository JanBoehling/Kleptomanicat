using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoomTrigger : AudioPlayer
{
    [SerializeField] private bool onlyTriggerOnce;

    private bool hasBeenTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyTriggerOnce && hasBeenTriggered) return;

        PlayAudio();
        hasBeenTriggered = true;
    }
}
