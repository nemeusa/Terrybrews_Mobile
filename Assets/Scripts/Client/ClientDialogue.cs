using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ClientDialogue : MonoBehaviour
{
    //[Header("Estados del cliente")]
    //public bool imposter;

    //[Header("Referencias externas")]
    ////public TextMeshProUGUI textCharla;

    //[Header("Diálogos neutrales")]
    ////public string[] charlaGeneral;

    //[Header("Diálogos por tema")]
    //public ThemeDialogue[] themeDialogues;

    //[HideInInspector] public string currentDialogue;
    //[HideInInspector] public string Theme;

    //Client _client;

    //private void Awake()
    //{
    //    _client = GetComponent<Client>();
    //}

    //public void Charla()
    //{
    //    bool useTheme = Random.Range(0, 101) > 50;

    //    if (useTheme)
    //    {
    //        string temaString = _client.player._talkTheme.currentTheme[_client.player._talkTheme._indexTheme];
    //        TalkTheme currentTheme = (TalkTheme)System.Enum.Parse(typeof(TalkTheme), temaString);
    //        TalkTheme themeToUse;

    //        if (!_client.imposter)
    //        {
    //            themeToUse = currentTheme; // Cliente bueno dice el tema actual
    //        }
    //        else
    //        {
    //            // Impostor elige un tema distinto
    //            List<TalkTheme> posibles = System.Enum.GetValues(typeof(TalkTheme))
    //                .Cast<TalkTheme>()
    //                .Where(t => t != currentTheme)
    //                .ToList();

    //            themeToUse = posibles[Random.Range(0, posibles.Count)];
    //        }

    //        string frase = GetPhraseByTheme(themeToUse);
    //        //textCharla.text = frase;
    //        _client.textCharla.text = frase;
    //        currentDialogue = frase;
    //        Theme = themeToUse.ToString();

    //        Debug.Log($"Tema: {Theme} - Impostor: {_client.imposter}");
    //    }
    //    else
    //    {
    //        // Frase neutral
    //        _client.Charla();
    //    }
    //}

    //private string GetPhraseByTheme(TalkTheme theme)
    //{
    //    foreach (var entry in themeDialogues)
    //    {
    //        if (entry.theme == theme)
    //        {
    //            if (entry.phrases.Length == 0) return "[sin frases]";
    //            return entry.phrases[Random.Range(0, entry.phrases.Length)];
    //        }
    //    }

    //    return "[tema no encontrado]";
    //}
}


//[System.Serializable]
//public class ThemeDialogue
//{
//    public TalkTheme theme;
//    [TextArea(2, 5)] public string[] phrases;
//}

//public enum TalkTheme
//{
//    Frio,
//    Calor,
//    Trafico,
//    Despejado
//}