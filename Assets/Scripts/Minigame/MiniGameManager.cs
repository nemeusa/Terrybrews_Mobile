using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    int coinsHave;

    [SerializeField] TMP_Text coinText;

    [SerializeField] GameObject winMenu;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (coinsHave >= 10) winMenu.SetActive(true);
    }

    public void AddCoin(int newCoin)
    {
        coinsHave += newCoin;

        coinText.text = $"Coins {coinsHave}/10";
    }
}
