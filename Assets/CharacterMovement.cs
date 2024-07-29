using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the character in units per second
    public float targetZ = 300f; // Target position on the Z axis
    public float moveSpeed = 2f; // Speed of moving left and right
    private bool isMoving = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private Animator animator;
    private bool isColliding = false;
    private float originalSpeed;

    private Collider currentObstacleCollider;

    // Cached Transform component
    private Transform playerTransform;

    void Start()
    {
        playerTransform = transform; // Cache the Transform component
        animator = GetComponent<Animator>();
        animator.ResetTrigger("Run");
        animator.SetTrigger("Run");
        originalSpeed = speed;
    }

    void Update()
    {
        if (isMoving && !isColliding)
        {
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
                canMoveLeft = false;
                canMoveRight = false;
            }
        }
        else
        {
            canMoveLeft = isMoving;
            canMoveRight = isMoving;
        }

        if (canMoveLeft && Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (canMoveRight && Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // Ensure the player stays on the ground during the stumble animation
        if (isColliding)
        {
            EnsurePlayerOnGround();
        }
    }

    void MoveLeft()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, Vector3.left, out hit, 0.1f))
        {
            if (hit.collider.CompareTag("Boundary"))
            {
                Debug.Log("Player collided with the left boundary.");
                return;
            }
        }
        playerTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    void MoveRight()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, Vector3.right, out hit, 0.1f))
        {
            if (hit.collider.CompareTag("Boundary"))
            {
                Debug.Log("Player collided with the right boundary.");
                return;
            }
        }
        playerTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
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
        canMoveLeft = true;
        canMoveRight = true;

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
        isMoving = true;
        canMoveLeft = true;
        canMoveRight = true;
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Run");
    }
}
