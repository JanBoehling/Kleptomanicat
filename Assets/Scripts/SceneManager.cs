using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int sceneId) => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);

    public void QuitGame() => Application.Quit();

    public void OpenLink(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            Debug.LogWarning("Could not open link as there was no link set.");
            return;
        }

        Application.OpenURL(link);
    }
}
