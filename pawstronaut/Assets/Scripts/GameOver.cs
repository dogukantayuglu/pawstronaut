using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text endGameFoodText;
    public Text inGameFoodText;

    private float endGameFoodInt = 0;
    private int inGameFoodInt = 0;
    private bool canContinue;

    void Start()
    {
        canContinue = false;
    }

    void Update()
    {
        inGameFoodInt = Int32.Parse(inGameFoodText.text);

        if (!GameManager.gameIsActive && gameObject.activeSelf)
        {
            ShowEndGameFood();
        }
    }

    public void Restart()
    {
        if (canContinue)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void BackToMainMenu()
    {
        if (canContinue)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }

    public void TransferFoodToAccount()
    {
        var temp = PlayerPrefs.GetInt("Food", 0);
        temp += inGameFoodInt;
        PlayerPrefs.SetInt("Food", temp);
    }

    public void ShowEndGameFood()
    {
        if (endGameFoodInt < inGameFoodInt)
        {
            endGameFoodInt += inGameFoodInt * Time.deltaTime;
            endGameFoodText.text = ((int)endGameFoodInt).ToString();
        }
        else
        {
            canContinue = true;
        }
    }
}
