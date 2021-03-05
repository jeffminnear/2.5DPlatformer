using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text livesText;
    public Text coinText;
    public Text gameOverText;
    public int lives;
    public int coins;
    public bool gameIsOver = false;


    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (gameIsOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }


    public void UpdateCoins(int value)
    {
        coins += value;
        coinText.text = "Coins: " + coins;
    }

    public void UpdateLives(int value)
    {
        lives += value;
        if (lives < 0)
        {
            gameIsOver = true;
            livesText.text = "Lives: " + 0;
            gameOverText.enabled = true;
        }
        else
        {
            livesText.text = "Lives: " + lives;
        }
    }


    private void Initialize()
    {
        gameOverText.enabled = false;

        UpdateLives(2);
        UpdateCoins(0);
    }
}
