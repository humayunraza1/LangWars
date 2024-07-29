using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab
    public float spawnInterval = 2f; // Time between spawns
    public float minX = 0f; // Minimum X position for spawning
    public float maxX = 30f; // Maximum X position for spawning
    public float fixedY = 12.1f; // Fixed Y position for spawning
    public float minZ = 3f; // Minimum Z position for spawning
    public float maxZ = 300f; // Maximum Z position for spawning
    public int maxCoins = 10; // Maximum number of coins
    public float minDistance = 20f; // Minimum distance between coins
    public float maxDistance = 35f; // Maximum distance between coins
    public float rotationSpeed = 30f; // Speed of coin rotation in degrees per second

    private float nextSpawnTime;
    private CharacterMovement characterMovement; // Reference to the CharacterMovement script
    private List<GameObject> spawnedCoins = new List<GameObject>(); // List to store the spawned coin game objects

    void Start()
    {

        // Initialize the spawn time
        nextSpawnTime = Time.time;

        // Find the CharacterMovement script
        characterMovement = FindObjectOfType<CharacterMovement>();
        if (characterMovement == null)
        {
            Debug.LogError("CharacterMovement script not found in the scene.");
        }

        // Start the spawning process
        SpawnCoins();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Update the maxZ value based on the player's current targetZ position
            if (characterMovement != null)
            {
                maxZ = characterMovement.targetZ;
            }

            // If there are fewer than the maximum number of coins, spawn more
            if (spawnedCoins.Count < maxCoins)
            {
                SpawnCoin();
            }
            
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCoin()
    {
        Vector3 spawnPosition = Vector3.zero; // Initialize the variable
        bool validPosition = false;
        int attempts = 0;

        // Attempt to find a valid position for the new coin
        while (!validPosition && attempts < 100)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            spawnPosition = new Vector3(randomX, fixedY, randomZ);

            // Check if the position is at least minDistance from all existing coins
            validPosition = true;
            foreach (GameObject coin in spawnedCoins)
            {
                if (Vector3.Distance(spawnPosition, coin.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            attempts++;
        }

        // If a valid position is found, spawn the coin and start rotation
        if (validPosition)
        {
            GameObject coin = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation);
            spawnedCoins.Add(coin);
            coin.tag = "Coin";
            // Add a Rotator component to handle the rotation
            Rotator rotator = coin.AddComponent<Rotator>();
            rotator.rotationSpeed = rotationSpeed;
        }
    }

    void SpawnCoins()
    {
        // Initial spawning to ensure that there are coins at the start
        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }
    }

    public void ClearCoins()
    {
        // Destroy all previously spawned coins
        foreach (GameObject coin in spawnedCoins)
        {
            if (coin != null)
            {
                Destroy(coin);
            }
        }
        spawnedCoins.Clear();
    }
}
