using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int inGameFood;

    public float score;
    public float highScore;
    public Text scoreText;
    public Text highScoreText;
    public Text endgameScoreText;
    public Text foodText;

    private int foodTextInt;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();
        inGameFood = 0;
        foodText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        foodTextInt = int.Parse(foodText.text);
        if (inGameFood != foodTextInt)
        {
            foodText.text = inGameFood.ToString();
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            score += 1 * Time.deltaTime;
            scoreText.text = ((int)score).ToString();
            endgameScoreText.text = ((int)score).ToString();



            if ((int)score > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", (int)score);
                highScoreText.text = ((int)score).ToString();
            }

        }
    }
}
