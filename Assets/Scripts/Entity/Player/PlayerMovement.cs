using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    private float movementSpeed;

    private Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movementSpeed = GetComponent<PlayerAttributes>().movementSpeed;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        bool isWalking = horizontal != 0 || vertical != 0;
        if (isWalking) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        animator.SetBool("isWalking", isWalking);

        body.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;
    }
}
