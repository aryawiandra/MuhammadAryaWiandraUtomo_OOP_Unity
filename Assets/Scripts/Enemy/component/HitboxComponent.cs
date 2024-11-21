using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent health;
    private InvincibilityComponent invincibilityComponent;

    void Start()
    {
        health = GetComponent<HealthComponent>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
        if (health == null)
        {
            Debug.LogError("HealthComponent is required on the same GameObject.");
        }
    }

    public void Damage(Bullet bullet)
    {
        if (invincibilityComponent == null || !invincibilityComponent.isInvincible)
        {
            if (health != null)
            {
                health.Subtract(bullet.damage);
                invincibilityComponent.StartInvincibility();
                // Debug.Log("Player took " + bullet.damage + " damage from bullet.");


            }
        }
    }

    public void Damage(int damage)
    {
        if (invincibilityComponent == null || !invincibilityComponent.isInvincible)
        {
            if (health != null)
            {
                health.Subtract(damage);
                invincibilityComponent.StartInvincibility();
                // Debug.Log("Player took " + damage + " damage.");


            }
        }
    }
}