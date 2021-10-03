using UnityEngine;

public class FuelCan : MonoBehaviour
{
    public float speed;
    public float fuelAmount;

    private FuelSlider fuelSlider;
    private Spawner spawner;

    void Start()
    {
        fuelSlider = FindObjectOfType<FuelSlider>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        //Move objects down
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fuelSlider.AddFuel(fuelAmount);
            spawner.fuelSpawned = false;
            Destroy(gameObject);
        }
    }
}
