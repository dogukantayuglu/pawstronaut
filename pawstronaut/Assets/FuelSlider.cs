using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public float countdownMultiplier;

    public GameObject gameOverScreen;
    public GameManager gameManager;

    public int time = 50;

    // Update is called once per frame
    void Start()
    {
        SetMaxFuel(time);
        slider.value = slider.maxValue;
        SetGradientColorToMax();
        countdownMultiplier = 1.0f;
    }

    void Update()
    {
        if (slider.value > slider.minValue && GameManager.gameIsActive)
        {
            slider.value -= 1 * countdownMultiplier * Time.deltaTime;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        if (slider.value == 0)
        {
            gameManager.EndGame();
        }
    }

    public void SetGradientColorToMax()
    {
        fill.color = gradient.Evaluate(1f);
    }

    public void SetMaxFuel(int fuel)
    {
        slider.maxValue = fuel;
    }

    public void SetFuel(int fuel)
    {
        slider.value = fuel;
    }

    public void AddFuel(float fuel)
    {
        slider.value += fuel;
    }

    public void ReduceFuel(int fuel)
    {
        slider.value -= fuel;
    }

}

