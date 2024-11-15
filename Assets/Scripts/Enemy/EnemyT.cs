using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;
    private Transform player;
    private Vector3 screenBounds;
    public int health = 3; // Add health property


    void RandomizeSpawnPoint()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomizeSpawnPoint();
    }
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // Keep the enemy within screen bounds
        // Vector3 pos = transform.position;
        // pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        // pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);
        // transform.position = pos;
    }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;
    //     if (health <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            Destroy(collision.gameObject); // Destroy the bullet
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}