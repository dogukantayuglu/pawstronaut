using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Vector2 screenBounds;
    public static GameObject[] obstacles;
    public static bool gameIsActive;

    public GameObject gameOverPanel;
    public GameObject pauseMenu;    
    public GameObject player;
    public GameObject inGameScoreText;
    public GameObject inGameFoodText;

    private GameOver gameOver;
    private TimeManager timeManager;

    void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        gameIsActive = true;
        gameOver = GetComponent<GameOver>();
        timeManager = GetComponent<TimeManager>();
    }
    
    public void EndGame()
    {
        timeManager.SetTimeScale(1);
        player.SetActive(false);
        inGameScoreText.SetActive(false);
        inGameFoodText.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOver.TransferFoodToAccount();
        gameIsActive = false;
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseGame();
    }

    void PauseGame()
    {
        gameIsActive = !gameIsActive;
        timeManager.SetTimeScale(timeManager.GetTimeScale() > 0 ? 0 : 1);
    }
}
