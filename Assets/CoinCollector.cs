using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioClip coinSound; // Reference to the sound to play
    [SerializeField] private AudioSource audioSource; // Component to play the sound

    [SerializeField] private UIManager levelUI;
 public float volume = 0.6f; // Volume level of the sound (0.0 to 1.0)
    public float pitch = 0.7f; // Pitch level of the sound (default is 1.0)
    private int totalCoinsCollected = 0;
    private int totalScore = 0;
    private void Start() {
        audioSource.clip = coinSound; // Assign the audio clip to the audio source
        audioSource.time = 0.6f;
            audioSource.volume = volume; // Set the volume level
        audioSource.pitch = pitch; // Set the pitch level
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Coin")) {
            Debug.Log("Coin collected");
            audioSource.PlayOneShot(coinSound); // Play the coin sound
            other.gameObject.SetActive(false); // Disable the coin
            totalCoinsCollected++;
            levelUI.UpdateTotalCoinsUI(totalCoinsCollected);
            AddToScore(20);
        }
    }

        public void AddToScore(int score){
            totalScore = totalScore + score;
            levelUI.UpdatePlayerScoreUI(totalScore);
        }
        public int GetTotalCoinsCollected() {
        return totalCoinsCollected;
    }

        public int GetTotalScore(){
            return totalScore;
        }

        public void resetScores(){
            totalCoinsCollected = 0;
            totalScore = 0;
            levelUI.UpdatePlayerScoreUI(totalScore);
            levelUI.UpdateTotalCoinsUI(totalCoinsCollected);
        }
}
