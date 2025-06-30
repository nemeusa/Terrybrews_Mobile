using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _IOSAdUnitId = "Rewarded_iOS";
    string _currentAdUnitId;

    [SerializeField] ShopManager _shopManager;

    public void LoadRewardedAd()
    {
        Advertisement.Load(_currentAdUnitId, this); 
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_currentAdUnitId, this);
        LoadRewardedAd();
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
        if(placementId == _currentAdUnitId)
        {
            if(showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) Debug.Log($"Anuncio completado");
            else if(showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED)) Debug.Log($"Anuncio saltado");
            else if(showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN)) Debug.LogWarning($"Estado desconocido del anuncio");
            Debug.Log($"Anuncio completado: {placementId}, Estado: {showCompletionState}");
            // Aquí puedes otorgar la recompensa al jugador
            // Por ejemplo: Player.Instance.GiveReward();

            if (_shopManager != null)
            {
                _shopManager.DoblarPuntos();
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
