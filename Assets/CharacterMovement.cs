using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the character in units per second
    public float targetZ = 300f; // Target position on the Z axis
    private bool isMoving = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private Animator animator;
    private bool isColliding = false;
    private float originalSpeed;
    private Collider currentObstacleCollider;

    // Cached Transform component
    private Transform playerTransform;

    // Swipe duration
    public float swipeDuration = 0.5f;
    private Coroutine swipeCoroutine;

    private Vector3 startingPosition;

    void Start()
    {
        playerTransform = transform; // Cache the Transform component
        animator = GetComponent<Animator>();
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Run");
        originalSpeed = speed;

        // Set initial X position to 4.3 and store the starting position
        startingPosition = new Vector3(10.51f, playerTransform.position.y, playerTransform.position.z);
        playerTransform.position = startingPosition;
    }

    void Update()
    {
        if (isMoving && !isColliding)
        {   
            animator.SetTrigger("Run");
            float step = speed * Time.deltaTime; // Calculate distance to move
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, targetZ);
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, step);

            // Keep player rotation fixed
            playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

            if (Vector3.Distance(playerTransform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
                if (animator != null)
                {
                    animator.ResetTrigger("Run");
                    animator.SetTrigger("Idle");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveLeft)
        {
            if (swipeCoroutine != null) StopCoroutine(swipeCoroutine);
            swipeCoroutine = StartCoroutine(MoveToPosition(new Vector3(10.51f, playerTransform.position.y, playerTransform.position.z)));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveRight)
        {
            if (swipeCoroutine != null) StopCoroutine(swipeCoroutine);
            swipeCoroutine = StartCoroutine(MoveToPosition(new Vector3(29.21f, playerTransform.position.y, playerTransform.position.z)));
        }

        // Ensure the player stays on the ground during the stumble animation
        if (isColliding)
        {
            EnsurePlayerOnGround();
        }

        // Update movement availability based on position
        canMoveLeft = playerTransform.position.x != 10.51f;
        canMoveRight = playerTransform.position.x != 29.21f;
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = playerTransform.position;

        while (elapsedTime < swipeDuration)
        {
            playerTransform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / swipeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle") && !isColliding)
        {
            HandleObstacleCollision(collision);
        }
    }

    public void HandleObstacleCollision(Collision collision)
    {
        if (isColliding) return;

        isColliding = true;
        isMoving = false; // Stop moving
        speed = 1f; // Decrease speed to a very low value

        if (animator != null)
        {
            animator.ResetTrigger("Run");
            animator.SetTrigger("Stumble");
        }

        // Change the obstacle collider to isTrigger
        currentObstacleCollider = collision.collider;
        currentObstacleCollider.isTrigger = true;

        // Calculate the distance needed to move the player past the obstacle
        float obstacleZSize = collision.collider.bounds.size.z;
        Vector3 moveDirection = Vector3.forward * (obstacleZSize + 3f); // Move along Z axis with extra buffer

        // Start the coroutine to handle collision
        StartCoroutine(HandleCollisionCoroutine(moveDirection));
    }

    private IEnumerator HandleCollisionCoroutine(Vector3 moveDirection)
    {
        // Move player to exit collision
        float elapsedTime = 0f;
        float duration = 0.5f; // Duration to move past the obstacle
        Vector3 startPosition = playerTransform.position;
        Vector3 endPosition = startPosition + moveDirection;

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = endPosition;

        // Ensure player stays on the ground
        EnsurePlayerOnGround();

        // Wait for a short duration before resuming normal speed
        yield return new WaitForSeconds(0.5f);

        if (animator != null)
        {
            animator.ResetTrigger("Stumble");
            animator.SetTrigger("Run");
        }

        // Gradually increase speed back to original
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            speed = Mathf.Lerp(1f, originalSpeed, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        speed = originalSpeed;

        isColliding = false;
        isMoving = true;

        // Revert the obstacle collider back to its original state
        if (currentObstacleCollider != null)
        {
            currentObstacleCollider.isTrigger = false;
            currentObstacleCollider = null;
        }
    }

    private void EnsurePlayerOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, Vector3.down, out hit))
        {
            playerTransform.position = new Vector3(playerTransform.position.x, hit.point.y, playerTransform.position.z);
        }
    }

    public void ResetMovement()
    {
        // Reset position
        playerTransform.position = startingPosition;
        // Reset other variables
        isMoving = true;
        isColliding = false;
        speed = originalSpeed;
        if (animator != null)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Run");
        }
        // Stop any ongoing swipe coroutine
        if (swipeCoroutine != null) StopCoroutine(swipeCoroutine);
    }
}
