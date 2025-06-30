using System;
using UnityEngine;

public class DEBUGSumarVidas : MonoBehaviour
{
    [SerializeField] LifeSystem lifeSystem;
    public void SumarUnaVida()
    {
        
        if (lifeSystem == null)
        {
            Debug.LogWarning("No se encontró el sistema de vidas.");
            return;
        }
        var tipo = typeof(LifeSystem);
        var currentLivesField = tipo.GetField("_currentLives", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var maxLivesField = tipo.GetField("_maxLives", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        if (currentLivesField != null && maxLivesField != null)
        {
            int currentLives = (int)currentLivesField.GetValue(lifeSystem);
            int maxLives = (int)maxLivesField.GetValue(lifeSystem);

            if (currentLives < maxLives)
            {
                currentLives++;
                currentLivesField.SetValue(lifeSystem, currentLives);

                // Resetear tiempo de recarga si llegamos al máximo
                var nextRechargeField = tipo.GetField("_nextLifeRecharge", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (currentLives >= maxLives && nextRechargeField != null)
                {
                    nextRechargeField.SetValue(lifeSystem, System.DateTime.Now);
                    PlayerPrefs.SetString("ProximaVida", System.DateTime.Now.ToString());
                }

                // Guardar y actualizar UI
                tipo.GetMethod("GuardarDatos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(lifeSystem, null);
                tipo.GetMethod("UpdateUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(lifeSystem, null);

                Debug.Log("Sumaste una vida. Total actual: " + currentLives);
            }
            else
            {
                Debug.Log("Ya tenés el máximo de vidas.");
            }
        }
    }
}