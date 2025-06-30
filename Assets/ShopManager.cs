using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Texto de Monedas")]
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI textoMonedas;

    [Header("Paneles de Mejora")]
    [SerializeField] TextMeshProUGUI[] textosNivelCosto; 
    [SerializeField]
    string[] clavesMejora = 
        {
        "BreakChance", "MaxObjects", "GenTime", "RepairClick"
        };
    [SerializeField] private int[] nivelesMaximos = { 5, 7, 8, 8 };
    [SerializeField] private Button[] botonesComprar;

    [Header("Donación")]
    [SerializeField] private GameObject objetoDesbloqueable;
    [SerializeField] private TextMeshProUGUI textoDonacion;
    [SerializeField] private Slider progresoDonacion;

    public int MONEDAS_POR_DONACION = 100;
    public int META_DONACION = 1000;

    [Header("Escenas")]
    [SerializeField] string escenaJuego = "Juego";
    [SerializeField] string escenaMenu = "MainMenu";

    void Start()
    {
        ActualizarUI();
    }
    public void ComprarMejora(int index)
    {
        if (index < 0 || index >= clavesMejora.Length) return;

        string _clave = clavesMejora[index];
        int _monedas = PlayerPrefs.GetInt("Coins", 0);
        int _nivel = PlayerPrefs.GetInt("Upgrade_" + _clave + "Level", 0);

        // Validación para evitar gastar monedas si ya está en nivel máximo
        if (_nivel >= nivelesMaximos[index])
        {
            Debug.Log("Ya alcanzaste el nivel máximo de " + _clave);
            return;
        }

        int _costo = CalcularCosto(_nivel);

        if (_monedas >= _costo)
        {
            _monedas -= _costo;
            _nivel++;
            PlayerPrefs.SetInt("Coins", _monedas);
            PlayerPrefs.SetInt("Upgrade_" + _clave + "Level", _nivel);
            PlayerPrefs.Save();
            ActualizarUI();
        }
    }
    public void Convertir()
    {
        int _puntos = PlayerPrefs.GetInt("Points", 0);
        int _monedas = PlayerPrefs.GetInt("Coins", 0);

        if (_puntos >= 100)
        {
            int _monedasGanadas = _puntos / 100;
            int _puntosActuales = _puntos % 100;

            _monedas += _monedasGanadas;
            _puntos = _puntosActuales;

            PlayerPrefs.SetInt("Points", _puntos);
            PlayerPrefs.SetInt("Coins", _monedas);
            PlayerPrefs.Save();

            Debug.Log("Convertidos " + _monedasGanadas * 100 + " puntos en " + _monedasGanadas + " monedas");
        }
        else
        {
            Debug.Log("No hay suficientes puntos para convertir");
        }

        ActualizarUI();
    }
    int CalcularCosto(int _nivel)
    {
        return 10 + _nivel * 10;
    }

    public void Donar()
    {
        int monedas = PlayerPrefs.GetInt("Coins", 0);
        int donado = PlayerPrefs.GetInt("MonedasDonadas", 0);

        if (monedas >= MONEDAS_POR_DONACION)
        {
            monedas -= MONEDAS_POR_DONACION;
            donado += MONEDAS_POR_DONACION;

            PlayerPrefs.SetInt("Coins", monedas);
            PlayerPrefs.SetInt("MonedasDonadas", donado);
            PlayerPrefs.Save();

            Debug.Log("Total donado: " + donado);

            if (donado >= META_DONACION && objetoDesbloqueable != null)
            {
                objetoDesbloqueable.SetActive(true);
            }      
        }
        else
        {
            Debug.Log("No tenés suficientes monedas para donar.");
        }
        
        ActualizarUI();
    }
    void ActualizarUI()
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        int _monedas = PlayerPrefs.GetInt("Coins", 0);
        textoMonedas.text = "Monedas: " + _monedas;

        for (int i = 0; i < clavesMejora.Length; i++)
        {
            string _clave = clavesMejora[i];
            int _nivel = PlayerPrefs.GetInt("Upgrade_" + _clave + "Level", 0);
            int _costo = CalcularCosto(_nivel);

            if (_nivel >= nivelesMaximos[i])
            {
                textosNivelCosto[i].text = "Nivel: MAX";
                if (botonesComprar.Length > i)
                    botonesComprar[i].interactable = false;
            }
            else
            {
                textosNivelCosto[i].text = "Nivel: " + _nivel + "\nCosto: " + _costo;
                if (botonesComprar.Length > i)
                    botonesComprar[i].interactable = true;
            }
        }

        int donado = PlayerPrefs.GetInt("MonedasDonadas", 0);
        if (progresoDonacion != null)
        {
            progresoDonacion.value = donado;
        }
    }
    public void VolverAJuego()
    {
        SceneManager.LoadScene(escenaJuego);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(escenaMenu);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            SumarMonedas(1000);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarMejoras();
        }
    }
    void SumarMonedas(int cantidad)
    {
        int monedas = PlayerPrefs.GetInt("Coins", 0);
        monedas += cantidad;
        PlayerPrefs.SetInt("Coins", monedas);
        PlayerPrefs.Save();
        Debug.Log("Monedas actuales: " + monedas);
        ActualizarUI();
    }

    void ReiniciarMejoras()
    {
        PlayerPrefs.DeleteKey("Upgrade_BreakChanceLevel");
        PlayerPrefs.DeleteKey("Upgrade_MaxObjectsLevel");
        PlayerPrefs.DeleteKey("Upgrade_GenTimeLevel");
        PlayerPrefs.DeleteKey("Upgrade_RepairClickLevel");
        PlayerPrefs.DeleteKey("BurstDesbloqueado");
        ActualizarUI();
        Debug.Log("Todas las mejoras han sido reiniciadas.");
    }

    public void DoblarPuntos()
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        puntos *= 2;
        PlayerPrefs.SetInt("Points", puntos);
        PlayerPrefs.Save();
    }
}