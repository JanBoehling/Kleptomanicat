using UnityEngine;

public class SpecialDoor : Interactable
{
    [Header("SpecialDoor"), SerializeField] private AudioClip onUnlockVoiceline;

    [Header("Debug")]
    [SerializeField] private bool hasKey;

    public override void Interact()
    {
        if (!hasKey) return;

        base.Interact();

        Application.Quit(69); //ToDo: implement end game stuff
    }

    public void UnlockDoor()
    {
        hasKey = true;

        onAudioEndedEvent.AddListener(null); // ToDo: call end game stuff here

        StartCoroutine(PlayAudio(onUnlockVoiceline));
    }
}
