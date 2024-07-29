using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // The obstacle prefab to spawn
    public Transform player;  // Reference to the player for spawning obstacles ahead of the player
    public Transform finishLine;  // Reference to the finish line
    public float spawnDistance = 50f;  // Distance ahead of the player to start spawning obstacles
    public float minDistanceFromCoins = 5f;  // Minimum distance from coins to spawn obstacles
    public float groundCheckDistance = 10f;  // Distance for raycasting to check the ground
    public int maxObstaclesPerWave = 7;  // Maximum number of obstacles to spawn in a wave
    public float gapDistanceMin = 25f;  // Minimum gap distance between waves
    public float gapDistanceMax = 35f;  // Maximum gap distance between waves
    public LayerMask groundLayer;  // Layer mask for the ground

    private float lastSpawnZ = 0f;
    private bool useFirstLane = true;
    private bool inGap = false;
    private int obstaclesInCurrentWave = 0;
    private float currentWaveEndZ = 0f;

    void Update()
    {
        if (Vector3.Distance(player.position, finishLine.position) > spawnDistance)
        {
            if (!inGap)
            {
                SpawnObstacleWave();
                inGap = true;
                currentWaveEndZ = lastSpawnZ;
            }
            else if (player.position.z + spawnDistance >= currentWaveEndZ + Random.Range(gapDistanceMin, gapDistanceMax))
            {
                inGap = false;
            }
        }
    }

    void SpawnObstacleWave()
    {
        obstaclesInCurrentWave = Random.Range(4, 8);  // Number of obstacles in this wave
        for (int i = 0; i < obstaclesInCurrentWave; i++)
        {
            // Alternate between lane positions 6 and 24
            float lanePositionX = useFirstLane ? 6f : 24f;

            // Calculate the tentative spawn position based on the player's position
            float spawnZ = lastSpawnZ + Random.Range(15f, 20f);
            Vector3 tentativeSpawnPosition = new Vector3(lanePositionX, player.position.y, spawnZ);

            // Use raycasting to find the ground position
            RaycastHit hit;
            if (Physics.Raycast(tentativeSpawnPosition + Vector3.up * groundCheckDistance, Vector3.down, out hit, groundCheckDistance * 2, groundLayer))
            {
                Vector3 spawnPosition = hit.point;

                // Check for nearby coins and find a valid spawn position
                Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, minDistanceFromCoins);
                bool isNearCoin = false;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Coin"))
                    {
                        isNearCoin = true;
                        break;
                    }
                }

                if (!isNearCoin)
                {
                    // Instantiate the obstacle at the spawn position
                    Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                    lastSpawnZ = spawnPosition.z;
                    useFirstLane = !useFirstLane;
                }
            }
        }
    }
}
