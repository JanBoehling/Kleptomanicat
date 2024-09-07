using UnityEngine;

public class SpecialDoor : RoomTrigger
{
    [Header("SpecialDoor")]
    [SerializeField] private AudioClip onUnlockVoiceline;
    [SerializeField] private AudioClip onGameEndVoiceline;

    [Header("Debug")]
    [SerializeField] private bool hasKey;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (hasKey) CuriousEnding();
    }

    public void UnlockDoor()
    {
        hasKey = true;

        StartCoroutine(PlayAudio(onUnlockVoiceline));
    }

    private void CuriousEnding()
    {
        Debug.LogWarning($"{GetType()}: Game Ending not implemented yet");

        StartCoroutine(PlayAudio(onGameEndVoiceline));


    }
}
