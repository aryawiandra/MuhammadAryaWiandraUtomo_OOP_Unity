
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Animator sceneTransitionAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTransition()
    {
        sceneTransitionAnimator.SetTrigger("StartTransition");
    }

    public void EndTransition()
    {
        sceneTransitionAnimator.SetTrigger("EndTransition");
    }
}