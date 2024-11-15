using UnityEngine;

public class EnemyBoss : Enemy
{
    // ...existing code...
    public float speed = 5f;
    public Weapon weapon; // Tambahkan properti weapon
    private Vector3 direction;

    private Vector3 spawnPoint;
    private AttackComponent attackComponent; // Tambahkan properti attackComponent
    void Start()
    {
        // ...existing code...

        attackComponent = GetComponent<AttackComponent>(); // Initialize AttackComponent
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        transform.position = spawnPoint;
        weapon = GetComponent<Weapon>(); // Inisialisasi weapon
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the enemy is off the screen and reverse direction
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (transform.position.x > screenBounds.x)
        {
            direction = -direction;
        }
        else if (transform.position.x < -screenBounds.x)
        {
            direction = -direction;
        }
        Shoot(); // Panggil metode untuk menembak
    }

    void Shoot()
    {
        if (weapon != null)
        {
            // weapon.Shoot(); // Gunakan metode Fire dari weapon
        }
    }
}