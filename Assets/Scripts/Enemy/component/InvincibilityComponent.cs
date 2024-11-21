using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    private HitboxComponent hitboxComponent;
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitboxComponent = GetComponent<HitboxComponent>();
        originalMaterial = spriteRenderer.material;
    }

    public void StartInvincibility()
    {
        // Debug.Log("Invincibility Started");
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;

            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;

            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;
    }
}