using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 60f; // Tiempo inicial en segundos
    public TextMeshProUGUI timerText;        // Asigna en el Inspector un Text UI
    public string nextSceneName;  // Nombre de la escena a cargar

    private float currentTime;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();


        if (currentTime <= 0f)
        {
            LoadNextScene();
        }
    }

    public void AddTime(float seconds)
    {
        currentTime += seconds;
    }

    public void SubtractTime(float seconds)
    {
        currentTime -= seconds;
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el nombre de la próxima escena.");
        }
    }
}
