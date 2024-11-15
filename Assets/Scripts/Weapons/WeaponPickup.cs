using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;
    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder);
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            // Initialize all related methods with false
            TurnVisual(false);
            weapon.transform.SetParent(transform, false);
            weapon.transform.localPosition = transform.position;
            weapon.parentTransform = transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon.transform.parent == transform && other.CompareTag("Player"))
        {
            // Get the current weapon and its pickup point
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
            if (currentWeapon != null)
            {
                currentWeapon.transform.SetParent(currentWeapon.parentTransform);
                currentWeapon.transform.localPosition = Vector3.zero;
                TurnVisual(false, currentWeapon);
            }

            // Assign the new weapon to the player
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = new Vector3(0, -0.05f, 0);
            TurnVisual(true);

        }
    }

    void TurnVisual(bool state)
    {
        if (weapon != null)
        {
            // Enable or disable all MonoBehaviour components in the Weapon object
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = state;
            }

            // Enable or disable the Animator component
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = state;
            }

            // Enable or disable the renderer components
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = state;
            }
        }
    }

    void TurnVisual(bool state, Weapon weapon)
    {
        if (weapon != null)
        {
            // Enable or disable all MonoBehaviour components in the Weapon object
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = state;
            }

            // Enable or disable the Animator component
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = state;
            }

            // Enable or disable the renderer components
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = state;
            }
        }
    }
}