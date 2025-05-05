using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    public float bonusTime = 5f;
    public float penaltyTime = 3f;

    private GameTimer gameTimer;

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus)) // "+" key (normal and numpad)
        {
            GoodAction();
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) // "-" key (normal and numpad)
        {
            BadAction();
        }
    }

    void GoodAction()
    {
        if (gameTimer != null)
        {
            gameTimer.AddTime(bonusTime);
            Debug.Log("Tiempo sumado: +" + bonusTime + "s");
        }
    }

    void BadAction()
    {
        if (gameTimer != null)
        {
            gameTimer.SubtractTime(penaltyTime);
            Debug.Log("Tiempo restado: -" + penaltyTime + "s");
        }
    }
}
