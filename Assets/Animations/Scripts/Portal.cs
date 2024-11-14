using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed; // seberapa cepat Portal Asteroid Bergerak
    [SerializeField] float rotateSpeed; // seberapa cepat Portal Asteroid Berputar
    Vector2 newPosition; // posisi yang dapat di-travel oleh asteroid
    Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        ChangePosition();
        animator = GetComponent<Animator>(); // Initialize the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        // Move the portal towards the new position
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Rotate the portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Check if player has weapon
        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            Debug.Log("Player has weapon, activating portal.");

            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;

        }
        else
        {
            Debug.Log("Player does not have weapon, deactivating portal.");

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset player's rotation
            // other.transform.rotation = Quaternion.identity;
            // Set the trigger parameter to play the animation

            // Enable Canvas and Image components
            foreach (Transform child in GameManager.Instance.transform)
            {
                if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    child.gameObject.SetActive(true);
                }
            }
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }


}