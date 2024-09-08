using UnityEngine;

public class CreditsMenuLoader : MonoBehaviour
{
    private AsyncOperation loadingOperation = null;

    private void Start()
    {
        loadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        loadingOperation.allowSceneActivation = false;
    }

    public void ActivateNextScene() => loadingOperation.allowSceneActivation = true;
}
