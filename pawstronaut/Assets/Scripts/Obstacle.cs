using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int damage = 1;
    public float speed;


    private void Update()
    {
        //Move objects down
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
