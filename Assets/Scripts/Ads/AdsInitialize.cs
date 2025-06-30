using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitialize : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "5888517";
    [SerializeField] string _IOSGameId = "5888516";
    string _currentGameId;
    [SerializeField] bool _testMode = true;


    private void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _currentGameId = _androidGameId;
#elif UNITY_IOS
        _currentGameId = _IOSGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_currentGameId, _testMode, this);
            Debug.Log("Iniciando el sistema de anuncios");
        }
        else
        {
            Debug.Log("El sistema de anuncios ya ha sido inicializado o no es compatible con esta plataforma.");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Podemos usar los anuncios");
        AdsManager.Instance.canUseAds = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Error al cargar el sistema de anuncios, {error}: {message}");
    }
}
