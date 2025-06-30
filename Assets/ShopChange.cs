using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string _shop = "Shop";

    public void Play()
    {        
            UnityEngine.SceneManagement.SceneManager.LoadScene(_shop);
    }
}