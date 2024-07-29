using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true; // Ensure the Rigidbody's rotation is initially frozen
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Call a method in CharacterMovement to handle the collision
            var characterMovement = GetComponent<CharacterMovement>();
            if (characterMovement != null)
            {
                characterMovement.HandleObstacleCollision(collision);
            }
        }
    }
}
