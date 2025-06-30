using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{ 
    public string nombreEscena = "Juego";
    
    //public void Shop()
    //{
    //  LifeSystem vidaSistema = FindObjectOfType<LifeSystem>();

    //        if (vidaSistema != null && vidaSistema.IntentarJugar())
    //        {
    //            UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
    //        }
    //        else
    //        {
    //        Debug.Log("No se puede jugar. Sin vidas.");           
    //        }
    //}
    public void Exit()
    {
        Application.Quit();
    }
}
