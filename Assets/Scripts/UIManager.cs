using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coinText;

    private int coins;

    void Start()
    {
        Initialize();
    }


    public void UpdateCoins(int value)
    {
        coins += value;
        coinText.text = "Coins: " + coins;
    }


    private void Initialize()
    {
        UpdateCoins(0);
    }
}
