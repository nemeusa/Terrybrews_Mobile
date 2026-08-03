using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void ChangeScene(string nextScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);

    }
}
