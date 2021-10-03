using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text foodText;

    void Start()
    {
        foodText.text = "Food: " + PlayerPrefs.GetInt("Food",0);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void ResetFood()
    {
        PlayerPrefs.DeleteKey("Food");
        foodText.text = "Food: " + PlayerPrefs.GetInt("Food", 0);
    }
}
