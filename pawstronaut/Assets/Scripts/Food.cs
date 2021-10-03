using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 1;
    public float speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().health += health;
            ScoreManager.inGameFood += 5;
            Destroy(gameObject);
        }
    }
}
