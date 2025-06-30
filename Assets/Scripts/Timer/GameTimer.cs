using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Tiempo")]
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;       
    private float currentTime;

    [Header("UI Menú de Derrota")]
    [SerializeField] private GameObject derrotaPanel;
    [SerializeField] private Button btnReintentar;
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnTienda;

    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string tiendaSceneName = "Shop";

    void Start()
    {
        currentTime = totalTime;

        if (btnReintentar != null)
            btnReintentar.onClick.AddListener(Retry);

        if (btnMenu != null)
            btnMenu.onClick.AddListener(() => LoadScene(menuSceneName));

        if (btnTienda != null)
            btnTienda.onClick.AddListener(() => LoadScene(tiendaSceneName));

        Time.timeScale = 1f; // Asegurarse de que el tiempo está corriendo al inicio
    }

    void Update()
    {
        Debug.Log(Time.timeScale);

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

    public void RestarTiempo(float cantidad)
    {
        currentTime -= cantidad;
        if (currentTime < 0) currentTime = 0;
    }
    void Retry()
    {
        Time.timeScale = 1f;
        Debug.Log("retry");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadScene(string nombreEscena)
    {
        Debug.Log("cargando escena");
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
    }

    private void LoadNextScene()
    {
        if (derrotaPanel != null)
        {
            Time.timeScale = 0f; // Pausar el juego
            Debug.Log("cargando siguiente escena");
            derrotaPanel.SetActive(true);
        }
        //else Time.timeScale = 1f;
    }
}
