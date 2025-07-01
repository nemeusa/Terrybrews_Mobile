using System;
using UnityEngine;
using TMPro;

public class LifeSystem : MonoBehaviour
{
    public int _maxLives = 5;
    public float _reloadLifeTime = 5f;
    [SerializeField] private TextMeshProUGUI textoVidas;
    [SerializeField] private TextMeshProUGUI textoRecarga;
    [SerializeField] private TextMeshProUGUI monedasActuales;
    private int _currentLives;
    private DateTime _nextLifeRecharge;

    void Start()
    {
        LoadData();
        InvokeRepeating(nameof(LifeChargeCheck), 0f, 1f);
        UpdateUI();
    }

    void LifeChargeCheck()
    {
        if (_currentLives >= _maxLives) return;

        if (DateTime.Now >= _nextLifeRecharge)
        {
            _currentLives++;
            if (_currentLives < _maxLives)
            {
                _nextLifeRecharge = DateTime.Now.AddMinutes(_reloadLifeTime);
                PlayerPrefs.SetString("ProximaVida", _nextLifeRecharge.ToString());
            }
            GuardarDatos();
        }
        UpdateUI();
    }

    public bool IntentarJugar()
    {
        if (_currentLives > 0)
        {
            PerderVida(); // Resta una vida
            return true;  // Se puede jugar
        }
        else
        {
            Debug.Log("Sin vidas, no se puede jugar.");
            // Acá podrías abrir un panel de espera o tienda
            return false;
        }
    }


    public void PerderVida()
    {
        if (_currentLives <= 0) return;

        _currentLives--;

        if (_currentLives < _maxLives && _currentLives == _maxLives - 1)
        {
            _nextLifeRecharge = DateTime.Now.AddMinutes(_reloadLifeTime);
            PlayerPrefs.SetString("ProximaVida", _nextLifeRecharge.ToString());
        }

        GuardarDatos();
        UpdateUI();
    }

    void UpdateUI()
    {
        textoVidas.text = "" + _currentLives;

        if (_currentLives >= _maxLives)
        {
            textoRecarga.text = "¡Máx!";
        }
        else
        {
            TimeSpan restante = _nextLifeRecharge - DateTime.Now;
            if (restante.TotalSeconds > 0)
            {
                textoRecarga.text = $"Next life: {restante.Minutes:D2}:{restante.Seconds:D2}";
            }
            else
            {
                textoRecarga.text = "Recargando...";
            }
        }

        if (monedasActuales != null)
        {
            int monedas = PlayerPrefs.GetInt("Coins", 0);
            monedasActuales.text = "" + monedas;
        }
    }

    void LoadData()
    {
        _currentLives = PlayerPrefs.GetInt("Vidas", _maxLives);

        string fechaGuardada = PlayerPrefs.GetString("ProximaVida", "");
        if (!string.IsNullOrEmpty(fechaGuardada))
        {
            _nextLifeRecharge = DateTime.Parse(fechaGuardada);
        }
        else
        {
            _nextLifeRecharge = DateTime.Now;
        }

        // Calcula cuántas vidas se deberían haber regenerado
        while (_currentLives < _maxLives && DateTime.Now >= _nextLifeRecharge)
        {
            _currentLives++;
            _nextLifeRecharge = _nextLifeRecharge.AddMinutes(_reloadLifeTime);
        }

        if (_currentLives >= _maxLives)
        {
            _nextLifeRecharge = DateTime.Now;
        }
    }

    void GuardarDatos()
    {
        PlayerPrefs.SetInt("Vidas", _currentLives);
        PlayerPrefs.SetString("ProximaVida", _nextLifeRecharge.ToString());
        PlayerPrefs.Save();
    }

    // Para testeo en editor
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) PerderVida(); // Simula perder vida
    }
}
