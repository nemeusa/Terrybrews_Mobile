using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    [Header("Diálogos por tema")]
    public ThemeDialogue[] themeDialogues;

    [HideInInspector] public string currentDialogue;
    [HideInInspector] public string Theme;

    [SerializeField] ClientOG _client;

    bool useTheme;

    string temaString;
    TalkTheme currentTheme;
    TalkTheme themeToUse;

    private void Awake()
    {
        useTheme = Random.Range(0, 101) > 50;
        //useTheme = true;
        _client = GetComponent<ClientOG>();
    }

    public void Charla()
    {
       // bool useTheme = Random.Range(0, 101) > 50;

        if (useTheme)
        {
            temaString = _client.player._talkTheme.currentTheme[_client.player._talkTheme._indexTheme];
            currentTheme = (TalkTheme)System.Enum.Parse(typeof(TalkTheme), temaString);
            //themeToUse;

            if (!_client.imposter)
            {
                themeToUse = currentTheme; // Cliente bueno dice el tema actual
            }
            else
            {
                // Impostor elige un tema distinto
                List<TalkTheme> posibles = System.Enum.GetValues(typeof(TalkTheme))
                    .Cast<TalkTheme>()
                    .Where(t => t != currentTheme)
                    .ToList();

                themeToUse = posibles[Random.Range(0, posibles.Count)];
            }

            string frase = GetPhraseByTheme(themeToUse);
            //textCharla.text = frase;
            _client.textCharla.text = frase;
            currentDialogue = frase;
            Theme = themeToUse.ToString();

            //Debug.Log($"Tema: {Theme} - Impostor: {_client.imposter}");
        }
        else
        {
            // Frase neutral
            _client.Charla();
        }
            Debug.Log("Tema" + themeToUse + "Impostor" + currentTheme);
    }

    public void Verification()
    {
        if ( useTheme)
        {
            if (_client.imposter && themeToUse == currentTheme)
            {
                Debug.Log("haciendolo malo");

            }



            else if (!_client.imposter && themeToUse != currentTheme)
            {
                Debug.Log("haciendolo bueno");

            }
             //   Debug.Log("verificacion");

        }

    }

    private string GetPhraseByTheme(TalkTheme theme)
    {
        foreach (var entry in themeDialogues)
        {
            if (entry.theme == theme)
            {
                if (entry.phrases.Length == 0) return "[sin frases]";
                return entry.phrases[Random.Range(0, entry.phrases.Length)];
            }
        }

        return "[tema no encontrado]";
    }
}


[System.Serializable]
public class ThemeDialogue
{
    public TalkTheme theme;
    public ThemeType type;
    [TextArea(2, 5)] public string[] phrases;
}

public enum TalkTheme
{
    Frio,
    Calor,
    Trafico,
    Despejado
}