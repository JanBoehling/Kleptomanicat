using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AudioPlayer : MonoBehaviour
{
    [Header("AudioPlayer"), SerializeField] private AudioClip voicelineToPlay;

    protected UnityEvent onAudioEndedEvent = new();

    private AudioSource audioSource;

    protected virtual void Awake()
    {
        if (!PlayerMovement.Instance.TryGetComponent(out audioSource)) Debug.LogWarning($"{GetType()}: Could not fetch Audio Source component from player");
    }

    protected void PlayAudio()
    {
        if (voicelineToPlay) StartCoroutine(PlayAudio(voicelineToPlay));
    }

    protected IEnumerator PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

        Debug.Log($"{GetType()}: Playing {clip}");

        yield return new WaitForSeconds(audioSource.clip.length);

        onAudioEndedEvent?.Invoke();
    }
}
