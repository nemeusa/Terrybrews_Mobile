using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{ 
    public string nombreEscena = "Juego";
    
    public void Play()
    {
      LifeSystem vidaSistema = FindObjectOfType<LifeSystem>();

            if (vidaSistema != null && vidaSistema.IntentarJugar())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
            }
            else
            {
            Debug.Log("No se puede jugar. Sin vidas.");           
            }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
