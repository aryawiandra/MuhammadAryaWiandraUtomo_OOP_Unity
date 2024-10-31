using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;

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

    void Start()
    {
        // Get component PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
        
        // get Animator dari EngineEffect
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        // Call method Move dari PlayerMovement
        if (playerMovement != null)
        {
            playerMovement.Move();
        }
    }

    void LateUpdate()
    {
        // Mengatur animator parameter IsMoving
        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
