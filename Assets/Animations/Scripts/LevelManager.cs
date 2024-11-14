using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        animator.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        // Play animation if needed
        if (animator != null)
        {
            //animator.SetTrigger("Start");
        }

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f); // Adjust the wait time to match the animation duration

        // Load the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        animator.SetTrigger("EndTransition");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

    }
}