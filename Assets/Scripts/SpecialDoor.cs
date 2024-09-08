using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpecialDoor : RoomTrigger
{
    [Header("SpecialDoor")]
    [SerializeField] private AudioClip onUnlockVoiceline;
    [SerializeField] private AudioClip onGameEndVoiceline;
    [SerializeField] private float waitTimeToLoadNewScene = 4;

    [Header("Debug")]
    [SerializeField] private bool hasKey;

    private SceneManager sceneManager;

    protected override void Awake()
    {
        base.Awake();

        sceneManager = FindObjectOfType<SceneManager>();
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

        StartCoroutine(LoadSceneAfterWaitTime());
    }

    private IEnumerator LoadSceneAfterWaitTime()
    {
        yield return new WaitForSeconds(waitTimeToLoadNewScene);

        sceneManager.LoadScene(2);
    }
}
