using UnityEngine;

public class PlayerProgressionController : MonoSingleton<PlayerProgressionController>
{
    [SerializeField] private AudioClip[] progressionClips;
    [SerializeField] private int currentClip;

    [SerializeField] private AudioSource audioPlayer;

    protected override void Awake()
    {
        base.Awake();
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayNextClip()
    {
        if (currentClip >= progressionClips.Length) return;

        audioPlayer.clip = progressionClips[currentClip];
        audioPlayer.Play();

        currentClip++;
    }
}
