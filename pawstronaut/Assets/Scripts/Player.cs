using UnityEngine;


public class Player : MonoBehaviour
{
    public float playerSpeed;
    public int health;
    public bool followTouchActive;

    [HideInInspector]
    public TouchPhase touchPhase;

    private TimeManager timeManager;
    private GameManager gameManager;

    private Vector3 pos;
    private Vector3 touchDistanceToPlayer;
    private Vector3 targetPosition;
    private float step;
    private float targetXPosition;
    private float targetYPosition;
    private float halfObjectWidth;
    private float halfObjectHeight;



    void Start()
    {
        timeManager = GetComponentInParent<TimeManager>();
        timeManager.SetTimeScale(0.01f);

        gameManager = GetComponentInParent<GameManager>();

        halfObjectWidth = (GetComponent<SpriteRenderer>().bounds.size.x) / 2;
        halfObjectHeight = (GetComponent<SpriteRenderer>().bounds.size.y) / 2;
        transform.position = new Vector3(0, -(GameManager.screenBounds.y) + 1, 0);

        followTouchActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsActive)
        {
            SlowTimeIfNoTouch();
            if (followTouchActive)
            {
                FollowTouchMove();
            }
            else
            {
                DistantTouchMove();
            }
        }
    }

    void DistantTouchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPhase = touch.phase;
            pos = Camera.main.ScreenToWorldPoint(touch.position);
            pos.z = 0f;
            step = playerSpeed * Time.deltaTime;



            if (touchPhase == TouchPhase.Began)
            {
                touchDistanceToPlayer = new Vector3(pos.x - transform.position.x, pos.y - transform.position.y, 0f);
                targetPosition = transform.position;
            }

            if (touchPhase == TouchPhase.Moved)
            {
                if (pos.x - touchDistanceToPlayer.x < GameManager.screenBounds.x - halfObjectWidth &&
                    pos.x - touchDistanceToPlayer.x > -(GameManager.screenBounds.x - halfObjectWidth))
                {
                    targetXPosition = pos.x - touchDistanceToPlayer.x;
                }
                if (pos.y - touchDistanceToPlayer.y < GameManager.screenBounds.y - halfObjectHeight &&
                    pos.y - touchDistanceToPlayer.y > -(GameManager.screenBounds.y - halfObjectHeight))
                {
                    targetYPosition = pos.y - touchDistanceToPlayer.y;
                }

                targetPosition = new Vector3(targetXPosition, targetYPosition, 0f);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }

            if (touchPhase == TouchPhase.Stationary)
            {
                if (transform.position == targetPosition)
                {
                    targetPosition = transform.position;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                }
            }

            if (touchPhase == TouchPhase.Ended)
            {
                targetPosition = transform.position;
            }
        }

    }

    void FollowTouchMove()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPhase = touch.phase;

            pos = Camera.main.ScreenToWorldPoint(touch.position);
            pos.z = 0f;

            step = playerSpeed * Time.deltaTime;
            if (pos.x < GameManager.screenBounds.x - halfObjectWidth &&
                pos.x > -(GameManager.screenBounds.x - halfObjectWidth) &&
                pos.y < GameManager.screenBounds.y - halfObjectWidth &&
                pos.y > -(GameManager.screenBounds.y - halfObjectWidth))
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, step);
            }
        }
    }

    void SlowTimeIfNoTouch()
    {
        if (touchPhase == TouchPhase.Ended)
        {
            timeManager.SetTimeScale(0.06f);
        }

        if (touchPhase == TouchPhase.Stationary || touchPhase == TouchPhase.Moved)
        {
            timeManager.SetTimeScale(1f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameManager.EndGame();
        }
    }
}
