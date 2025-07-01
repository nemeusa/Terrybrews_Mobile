using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;

    public void LoadBannerAd()
    {
        BannerLoadOptions bannerLoadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_androidAdUnitId, bannerLoadOptions);
    }

    public void RequestBanner()
    {
        LoadBannerAd();
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(_androidAdUnitId);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner cargado correctamente.");
    }

    private void OnBannerError(string message)
    {
        Debug.LogError($"Error al cargar el banner: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Anuncio cargado: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error al cargar el anuncio: {placementId}, Error: {error}, Mensaje: {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Anuncio clickeado: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Anuncio completado: {placementId}, Estado: {showCompletionState}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error al mostrar el anuncio: {placementId}, Error: {error}, Mensaje: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Anuncio iniciado: {placementId}");
    }
}
