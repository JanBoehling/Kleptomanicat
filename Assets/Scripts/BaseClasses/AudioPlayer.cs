using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [Header("AudioPlayer"), SerializeField] private AudioClip voicelineToPlay;

    protected UnityEvent onAudioEndedEvent;

    private AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void PlayAudio()
    {
        // Plays voice line when interacting
        if (!voicelineToPlay)
        {
            Debug.LogWarning($"{GetType()}: No audio clip set on object {name}");
            return;
        }
        
        StartCoroutine(PlayAudio(voicelineToPlay));
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
