using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f;
    private Vector3 direction;
    private Vector3 spawnPoint;

    private SpriteRenderer spriteRenderer; // Add SpriteRenderer reference
    private InvincibilityComponent invincibilityComponent; // Add Invincibility reference
    private AttackComponent attackComponent; // Add AttackComponent reference

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize SpriteRenderer
        invincibilityComponent = GetComponent<InvincibilityComponent>(); // Initialize Invincibility
        attackComponent = GetComponent<AttackComponent>(); // Initialize AttackComponent
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        transform.position = spawnPoint;
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
    }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;
    //     Debug.Log("Health: " + health);
    //     invincibilityComponent.StartInvincibility(); // Use Invincibility component's Blink method
    //     if (health <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}