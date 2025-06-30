using TMPro;
using UnityEngine;
using System.Collections;

public class TalksTeme : MonoBehaviour
{
    public string currentClima;
    public string currentEventos;
    public string[] currentTheme;
    [SerializeField] TMP_Text textTheme;
    public int _indexTheme;

    bool _newChanel;

    private void Start()
    {
        InitialTheme();
        RandomTheme();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V)) RandomTheme();
        //TalkTheme();
        if (!_newChanel)
        StartCoroutine(CambiaCanal());
    }

    void RandomTheme()
    {
        string charla;

        currentTheme = new string[] { currentClima, currentEventos } ;
        charla = currentTheme[_indexTheme];

        textTheme.text = charla;
        _indexTheme++;
        if (_indexTheme >= 2) _indexTheme = 0;
    }

    void InitialTheme()
    {
        string[] clima = { "Frio", "Calor" };
        string[] eventos = { "Trafico", "Despejado" };

        currentClima = clima[UnityEngine.Random.Range(0, clima.Length)];
        currentEventos = eventos[UnityEngine.Random.Range(0, eventos.Length)];
    }

    IEnumerator CambiaCanal()
    {
        _newChanel = true;
        yield return new WaitForSeconds(2);
        RandomTheme();
        _newChanel = false;
    }
}

public enum ThemeType
{
    Clima,
    Evento
}