using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject coinSpawner; // Reference to the coin spawner GameObject

    public Camera uiCamera; // Reference to the UI camera
    public Camera playerCamera; // Reference to the player camera

    [SerializeField] private GameObject PlayerObject;    
    public GameObject levelsCanvas;
    [SerializeField] private CoinCollector coinCollector;

    [SerializeField] private QuestionManager Qmanager;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CoinSpawner coinSpawnerScript;

    [SerializeField] private QuestionCubeSpawner questionSpawnerScript;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private PlayerLivesManager lives;
    void Start()
    {
        obstacleSpawner.enabled = false;
        // Disable the scripts at the start
        // Set the initial camera to the UI camera
        uiCamera.enabled = true;
        playerCamera.enabled = false;
    }

    // Call this method to start the level
    public void StartLevel()
    {
        // Enable the scripts
        PlayerObject.SetActive(true);
        obstacleSpawner.gameObject.SetActive(true);
        obstacleSpawner.SpawnObstacleWave();
        characterMovement.enabled = true;
        coinSpawnerScript.enabled = true;
        questionSpawnerScript.enabled = true;
        coinCollector.resetScores();
        // Spawn question cubes
        questionSpawnerScript.SpawnQuestionCubes();
        // Switch cameras
        uiCamera.enabled = false;
        playerCamera.enabled = true;
    }

    public void ChangeBackCamera()
    {
        characterMovement.ResetMovement();
        coinSpawnerScript.ClearCoins();
        questionSpawnerScript.ClearCubes();
       obstacleSpawner.ResetObstacles();
       Qmanager.ResetWrongAnswers();
        lives.ResetLives();
        obstacleSpawner.enabled = false;
        characterMovement.enabled = false;
        coinSpawnerScript.enabled = false;
        questionSpawnerScript.enabled = false;
        coinSpawnerScript.ClearCoins();
        questionSpawnerScript.ClearCubes();
        // Set the initial camera to the UI camera
        characterMovement.ResetMovement();

        uiCamera.enabled = true;
        playerCamera.enabled = false;
        levelsCanvas.SetActive(true);
    }

    // Method to reset the character movement
}