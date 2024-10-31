using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    void Start()
    {
        // Get component Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

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
    }

    public void Move()
    {
        // Menerima input
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection.normalized;

        // Friction untuk memperhalus gerakan (drift)
        Vector2 friction = GetFriction();

        // Update velocity untuk axis X
        if (moveDirection.x != 0)
        {
            // Acceleration
            float accelerationX = (2f * maxSpeed.x / timeToFullSpeed.x);
            moveVelocity.x += moveDirection.x * accelerationX * Time.fixedDeltaTime;
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        }
        else
        {
            // Deceleration
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

        // Update velocity untuk axis Y
        if (moveDirection.y != 0)
        {
            // Acceleration
            float accelerationY = (2f * maxSpeed.y / timeToFullSpeed.y);
            moveVelocity.y += moveDirection.y * accelerationY * Time.fixedDeltaTime;
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else
        {
            // Deceleration
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

        // Apply velocity ke pesawat
        rb.velocity = moveVelocity;
    }

    public Vector2 GetFriction()
    {
        // Return friction untuk setiap acis sesuai input horizontal/vertikal
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    public void MoveBound()
    {
        // empty dulu nanti bisa tambahin
        // ini biar ga bisa keluar screen
    }

    public bool IsMoving()
    {
        // Check pesawat IsMoving true/false
        return rb.velocity.magnitude > 0.01f;
    }
}
