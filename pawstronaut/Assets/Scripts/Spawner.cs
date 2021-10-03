using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public float startTimeBetweenSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    public GameObject[] obstacleList;
    public GameObject[] foodList;
    public GameObject[] skillList;
    public GameObject fuelCan;
    public FuelSlider fuelSlider;
    public bool fuelSpawned;

    private GameObject[] activeObjects;
    private BoxCollider2D boxCollider2D;
    private GameObject selectedObject;
    private Vector2 screenBounds;
    private float halfObjectSize;
    private float timeBetweenSpawn;
    private float forbiddenXPosition;
    private float randomX;
    private float outOfScreen;
    private int randomValue;



    // Start is called before the first frame update
    void Start()
    {
        //Set location where objects will be destroyed
        screenBounds = GameManager.screenBounds;
        outOfScreen = Math.Abs(screenBounds.y - (screenBounds.y * 3f));

        transform.position = new Vector3(0, screenBounds.y + 2, 1f);

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(screenBounds.x * 2, boxCollider2D.size.y - 0.9f);

        fuelSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetActiveObstacles();
        FindObstaclesCloseToSpawner();
        SpawnSelectedObject();
        DestroyGameObjects();
    }



    void SpawnSelectedObject()
    {
        if (timeBetweenSpawn <= 0)
        {
            SelectRandomObject();

            randomX = Random.Range(-screenBounds.x + halfObjectSize, screenBounds.x - halfObjectSize);

            var distance = Mathf.Abs(randomX - forbiddenXPosition);

            if (distance <= 1f)
            {
                return;
            }

            if (selectedObject == fuelCan)
            {
                fuelSpawned = true;
            }
            Instantiate(selectedObject, transform.position + new Vector3(randomX, 0, 0), Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn;


            if (startTimeBetweenSpawn > minTime)
            {
                startTimeBetweenSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    void SelectRandomObject()
    {
        if (fuelSlider.slider.value < 10 && !fuelSpawned)
        {
            selectedObject = fuelCan;
        }
        else
        {
            randomValue = Random.Range(1, 100);

            if (randomValue != 0)
            {
                if (randomValue >= 1 && randomValue <= 60)
                {
                    selectedObject = obstacleList[Random.Range(0, obstacleList.Length)];
                }
                else if (randomValue >= 61 && randomValue <= 80)
                {
                    selectedObject = foodList[Random.Range(0, foodList.Length)];
                }
                else if (randomValue >= 81 && randomValue <= 100)
                {
                    selectedObject = skillList[Random.Range(0, skillList.Length)];
                }
                halfObjectSize = (selectedObject.GetComponent<SpriteRenderer>().bounds.size.x) / 2;
            }
        }
    }



    void FindObstaclesCloseToSpawner()
    {
        foreach (var obstacle in activeObjects)
        {
            if (transform.position.y - obstacle.transform.position.y < 3)
            {
                forbiddenXPosition = obstacle.transform.position.x;
            }
        }
    }

    void DestroyGameObjects()
    {
        foreach (var obstacle in activeObjects)
        {
            if (Math.Abs(obstacle.transform.position.y) >= outOfScreen)
            {
                Destroy(obstacle.gameObject);
            }
        }
    }

    void GetActiveObstacles()
    {
        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        var foods = GameObject.FindGameObjectsWithTag("Food");
        var skills = GameObject.FindGameObjectsWithTag("Skill");
        activeObjects = (obstacles.Concat(foods).ToArray()).Concat(skills).ToArray();
    }
}
