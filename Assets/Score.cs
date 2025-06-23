using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntosTexto;

    void Update()
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        puntosTexto.text = "Puntos: " + puntos.ToString();
    }
}
