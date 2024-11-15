using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Apply damage to the enemy
            hitbox.Damage(damage);
        }

        objectPool.Release(this);
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnBecameInvisible()
    {
        // Add condition to check if the bullet should be released
        if (gameObject.activeSelf && objectPool != null)
        {
            objectPool.Release(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}