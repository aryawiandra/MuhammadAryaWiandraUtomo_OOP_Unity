using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed; // Speed of the portal movement
    [SerializeField] float rotateSpeed; // Rotation speed of the portal
    Vector2 newPosition; // Position that the portal will move towards
    Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        ChangePosition();
        animator = GetComponent<Animator>(); // Initialize the Animator component

        // Safety check for Animator
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on the portal. Make sure the portal has an Animator component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the portal towards the new position
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Rotate the portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // Change position when near target
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Check if player has a weapon to activate portal
        var weapon = GameObject.Find("Player")?.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            Debug.Log("Player has weapon, activating portal.");
            EnablePortal(true);
        }
        else
        {
            Debug.Log("Player does not have weapon, deactivating portal.");
            EnablePortal(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ensure GameManager and LevelManager exist before attempting to load the scene
            if (GameManager.Instance != null && GameManager.Instance.LevelManager != null)
            {
                // Enable UI components before loading the scene
                ActivateUIComponents(true);
                // Load the "Main" scene
                LoadSceneSafely("Main");
            }
            else
            {
                Debug.LogError("GameManager or LevelManager is not assigned. Cannot load scene.");
            }
        }
    }

    // Changes portal's position randomly
    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    // Enables or disables the portal's collider and sprite renderer
    void EnablePortal(bool isActive)
    {
        var collider = GetComponent<Collider2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (collider != null) collider.enabled = isActive;
        if (spriteRenderer != null) spriteRenderer.enabled = isActive;
    }

    // Activates UI components (e.g., Canvas, Image)
    void ActivateUIComponents(bool isActive)
    {
        foreach (Transform child in GameManager.Instance.transform)
        {
            var canvas = child.GetComponent<Canvas>();
            var image = child.GetComponent<UnityEngine.UI.Image>();

            if (canvas != null || image != null)
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }

    // Attempts to load a scene and logs an error if unsuccessful
    void LoadSceneSafely(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            Debug.Log("Scene already loaded: " + sceneName);
        }
        else if (SceneUtility.GetScenePathByBuildIndex(SceneUtility.GetBuildIndexByScenePath(sceneName)) != "")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene not found in build settings: " + sceneName);
        }
    }
}
