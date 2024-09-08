using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SpecialDoor : RoomTrigger
{
    [Header("SpecialDoor")]
    [SerializeField] private AudioClip onUnlockVoiceline;
    [SerializeField] private AudioClip onGameEndVoiceline;
    [SerializeField] private float waitTimeToLoadNewScene = 4;

    [Header("Debug")]
    [SerializeField] private bool hasKey;

    private Volume volume;
    private FilmGrain filmGrain;
    private LensDistortion lensDistortion;
    private ColorAdjustments colorAdjustments;
    private SceneManager sceneManager;

    private bool doEnding = false;
    private float endingTimer = 0;

    protected override void Awake()
    {
        base.Awake();

        sceneManager = FindObjectOfType<SceneManager>();
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet(out filmGrain);
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out colorAdjustments);
    }

    private void Update()
    {
        if (!doEnding)
            return;

        endingTimer += Time.deltaTime;

        float progress = endingTimer / onGameEndVoiceline.length;
        filmGrain.intensity.value = Mathf.Lerp(0, 1, progress);
        lensDistortion.intensity.value = Mathf.Lerp(0, 1, progress);
        colorAdjustments.saturation.value = Mathf.Lerp(0, -100, progress);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (hasKey)
            CuriousEnding();
        else
            base.OnTriggerEnter(other);
    }

    public void UnlockDoor()
    {
        hasKey = true;

        StartCoroutine(PlayAudio(onUnlockVoiceline));
    }

    private void CuriousEnding()
    {
        Debug.LogWarning($"{GetType()}: Game Ending not implemented yet");

        PlayerMovement.Instance.GetComponent<PlayerInput>().currentActionMap.Disable();
        StartCoroutine(PlayAudio(onGameEndVoiceline));

        doEnding = true;
        onAudioEndedEvent.AddListener(() => StartCoroutine(LoadSceneAfterWaitTime()));
    }

    private IEnumerator LoadSceneAfterWaitTime()
    {
        yield return new WaitForSeconds(waitTimeToLoadNewScene);

        sceneManager.LoadScene(2);
    }
}
