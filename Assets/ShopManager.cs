using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntosText;
    [SerializeField] private TextMeshProUGUI monedasText;

    private void Start()
    {
        ActualizarUI();
    }

    public void Convertir()
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        int monedas = PlayerPrefs.GetInt("Coins", 0);

        if (puntos >= 100)
        {
            int monedasGanadas = puntos / 100;
            puntos = puntos % 100;
            monedas += monedasGanadas;

            PlayerPrefs.SetInt("Points", puntos);
            PlayerPrefs.SetInt("Coins", monedas);
            PlayerPrefs.Save();

            Debug.Log("Convertidos puntos en " + monedasGanadas + " monedas");
        }
        else
        {
            Debug.Log("No hay suficientes puntos para convertir");
        }

        ActualizarUI();
    }

    public void Juego()
    {
        SceneManager.LoadScene("Play"); 
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu"); 
    }
    void ActualizarUI()
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        int monedas = PlayerPrefs.GetInt("Coins", 0);

        puntosText.text = "Puntos: " + puntos;
        monedasText.text = "Monedas: " + monedas;
    }
}
