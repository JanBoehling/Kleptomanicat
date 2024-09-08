using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeginningDoor : AudioPlayer
{
    [Header("Beginning Door")]
    [SerializeField] private float waitTimeToLoadNewScene = 4;

    private SceneManager sceneManager;

    protected override void Awake()
    {
        base.Awake();

        sceneManager = FindObjectOfType<SceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (!player)
            return;

        player.GetComponent<PlayerInput>().currentActionMap.Disable();
        PlayAudio();

        onAudioEndedEvent.AddListener(() => StartCoroutine(LoadSceneAfterWaitTime()));
    }

    private IEnumerator LoadSceneAfterWaitTime()
    {
        yield return new WaitForSeconds(waitTimeToLoadNewScene);

        FindObjectOfType<CreditsMenuLoader>().ActivateNextScene();
    }
}
