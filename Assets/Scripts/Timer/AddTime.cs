using UnityEngine;

public class AddTime : MonoBehaviour
{
    public float timeToAdd = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
        {
            timer.AddTime(timeToAdd);
            Destroy(gameObject); // Opcional: se destruye el objeto recolectado
        }
    }
}
