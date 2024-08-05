using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audio;
    private PlayerLivesManager livesManager;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.pitch = 0.7f;
        livesManager = GetComponent<PlayerLivesManager>();
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
                audio.PlayOneShot(clip);
            }
            livesManager.LoseLife();
        }
    }
}
