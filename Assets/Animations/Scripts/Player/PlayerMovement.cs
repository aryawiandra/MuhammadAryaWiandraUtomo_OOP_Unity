using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;
    [SerializeField] private Vector2 screenBoundOffset = Vector2.zero;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float objectWidth;
    private float objectHeight;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Inisialisasi main camera

        // Initialize moveVelocity
        moveVelocity = Vector2.zero;

        // Function moveFriction: -2 * maxSpeed / (timeToFullSpeed^2)
        moveFriction = new Vector2(
            -2f * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2f * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );

        // Function stopFriction: -2 * maxSpeed / (timeToStop^2)
        stopFriction = new Vector2(
            -2f * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2f * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );

        CalculateScreenBounds();
    }

    void CalculateScreenBounds()
    {
        if (mainCamera == null) return;

        // Menggunakan orthographic size untuk perhitungan yang lebih akurat
        float vertExtent = mainCamera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        // Mendapatkan ukuran sprite dengan padding yang lebih longgar
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.size.x / 2;
            objectHeight = spriteRenderer.bounds.size.y / 2;
        }

        // Set batas layar dengan offset dan padding tambahan
        float padding = 0.5f; // Menambah ruang gerak
        minX = -horizExtent + objectWidth + screenBoundOffset.x + padding;
        maxX = horizExtent - objectWidth - screenBoundOffset.x - padding;
        minY = -vertExtent + objectHeight + screenBoundOffset.y + padding;
        maxY = vertExtent - objectHeight - screenBoundOffset.y - padding;

        // Debug log untuk membantu troubleshooting
        Debug.Log($"Screen Bounds - MinX: {minX}, MaxX: {maxX}, MinY: {minY}, MaxY: {maxY}");
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // Receive input
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection.normalized;

        // Friction to smooth the movement (drift)
        Vector2 friction = GetFriction();

        // Update velocity for X axis
        if (moveDirection.x != 0)
        {
            float accelerationX = (2f * maxSpeed.x / timeToFullSpeed.x);
            moveVelocity.x += moveDirection.x * accelerationX * Time.fixedDeltaTime;
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        }
        else
        {
            float currentSpeedX = Mathf.Abs(moveVelocity.x);
            if (currentSpeedX < stopClamp.x)
            {
                moveVelocity.x = 0;
            }
            else
            {
                moveVelocity.x += Mathf.Sign(moveVelocity.x) * friction.x * Time.fixedDeltaTime;
            }
        }

        // Update velocity for Y axis
        if (moveDirection.y != 0)
        {
            float accelerationY = (2f * maxSpeed.y / timeToFullSpeed.y);
            moveVelocity.y += moveDirection.y * accelerationY * Time.fixedDeltaTime;
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else
        {
            float currentSpeedY = Mathf.Abs(moveVelocity.y);
            if (currentSpeedY < stopClamp.y)
            {
                moveVelocity.y = 0;
            }
            else
            {
                moveVelocity.y += Mathf.Sign(moveVelocity.y) * friction.y * Time.fixedDeltaTime;
            }
        }

        // Apply velocity to the player
        rb.velocity = moveVelocity;

        // Apply screen bounds
        MoveBound();
    }

    public void MoveBound()
    {
        Vector3 viewPos = transform.position;
        
        // Hanya membatasi posisi jika benar-benar melewati batas
        if (viewPos.x < minX)
        {
            viewPos.x = minX;
            moveVelocity.x = 0;
        }
        else if (viewPos.x > maxX)
        {
            viewPos.x = maxX;
            moveVelocity.x = 0;
        }

        if (viewPos.y < minY)
        {
            viewPos.y = minY;
            moveVelocity.y = 0;
        }
        else if (viewPos.y > maxY)
        {
            viewPos.y = maxY;
            moveVelocity.y = 0;
        }

        transform.position = viewPos;
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }

    // Menambahkan method untuk menghitung ulang batas saat ukuran layar berubah
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            CalculateScreenBounds();
        }
    }
}