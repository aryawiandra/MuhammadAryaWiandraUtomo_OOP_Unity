using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage;

    void OnTriggerEnter(Collider other)
    {


        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.Damage(damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var invincibilityComponent = collision.gameObject.GetComponent<InvincibilityComponent>();
        if (invincibilityComponent != null)
        {
            invincibilityComponent.StartInvincibility();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = GetComponent<HitboxComponent>();
        if (collision.gameObject.tag == gameObject.tag)
        {
            return;
        }
        if (collision.CompareTag("Bullet"))
        {
            // int damage = collision.GetComponent<Bullet>().damage; // Get damage from Bullet

            if (hitbox != null)
            {
                hitbox.Damage(collision.GetComponent<Bullet>()); // Apply damage using HitboxComponent with Bullet parameter
            }
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            hitbox = GetComponent<HitboxComponent>();
            if (hitbox != null)
            {
                hitbox.Damage(damage);
            }
        }

    }
}