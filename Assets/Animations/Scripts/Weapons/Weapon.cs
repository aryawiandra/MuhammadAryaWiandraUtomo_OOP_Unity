using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetActive(bool state)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = state;
        }
        gameObject.SetActive(state);
    }
}