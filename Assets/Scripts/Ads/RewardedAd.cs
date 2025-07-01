using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";

    [SerializeField] ShopManager _shopManager;

    public void LoadRewardedAd()
    {
        Advertisement.Load(_androidAdUnitId, this); 
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_androidAdUnitId, this);
        LoadRewardedAd();
    }

    private void Start()
    {
        if(_shopManager == null)
        {
            Debug.LogWarning("ShopManager no asignado. Si estamos fuera del Shop, no darle importancia a este mensaje");
        }
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
        if(placementId == _androidAdUnitId)
        {
            if(showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) Debug.Log($"Anuncio completado");
            else if(showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED)) Debug.Log($"Anuncio saltado");
            else if(showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN)) Debug.LogWarning($"Estado desconocido del anuncio");
            Debug.Log($"Anuncio completado: {placementId}, Estado: {showCompletionState}");
            // Aquí puedes otorgar la recompensa al jugador
            // Por ejemplo: Player.Instance.GiveReward();

            if (_shopManager != null)
            {
                _shopManager.TriplicarPuntos();
            }
            else
            {
                Debug.LogWarning("ShopManager no asignado");
            }
        }
        else
        {
            Debug.LogWarning($"El anuncio completado no coincide con el ID esperado: {placementId}");
        }
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
