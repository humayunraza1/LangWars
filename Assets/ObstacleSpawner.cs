using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // The obstacle prefab to spawn
    public Transform player;  // Reference to the player for spawning obstacles ahead of the player
    public Transform finishLine;  // Reference to the finish line
    public float spawnDistance = 50f;  // Distance ahead of the player to start spawning obstacles
    public float groundCheckDistance = 10f;  // Distance for raycasting to check the ground
    public int maxObstaclesPerWave = 7;  // Maximum number of obstacles to spawn in a wave
    public LayerMask groundLayer;  // Layer mask for the ground

    private float lastSpawnZ = 0f;
    private bool useFirstLane = true;
    private List<GameObject> spawnedObstacles = new List<GameObject>();

    void Start()
    {
        SpawnObstacleWave();
    }

    public void SpawnObstacleWave()
    {
        float finishLineZ = finishLine.position.z;
        while (lastSpawnZ + spawnDistance < finishLineZ)
        {
            int obstaclesInCurrentWave = Random.Range(4, 8);  // Number of obstacles in this wave
            for (int i = 0; i < obstaclesInCurrentWave; i++)
            {
                // Alternate between lane positions 6 and 24
                float lanePositionX = useFirstLane ? 10.6f : 29.6f;

                // Calculate the tentative spawn position based on the player's position
                float spawnZ = lastSpawnZ + Random.Range(15f, 20f);
                Vector3 tentativeSpawnPosition = new Vector3(lanePositionX, player.position.y, spawnZ);

                // Use raycasting to find the ground position
                RaycastHit hit;
                if (Physics.Raycast(tentativeSpawnPosition + Vector3.up * groundCheckDistance, Vector3.down, out hit, groundCheckDistance * 2, groundLayer))
                {
                    Vector3 spawnPosition = hit.point;

                    // Instantiate the obstacle at the spawn position
                    GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                    spawnedObstacles.Add(obstacle);  // Add to the list of spawned obstacles
                    lastSpawnZ = spawnPosition.z;
                    useFirstLane = !useFirstLane;

                    // Check if we reached the finish line zone
                    if (lastSpawnZ + spawnDistance >= finishLineZ)
                    {
                        break;
                    }
                }
            }

            // Add a gap after each wave
            lastSpawnZ += Random.Range(25f, 35f);
        }
    }

    public void ResetObstacles()
    {
        foreach (GameObject obstacle in spawnedObstacles)
        {
            Destroy(obstacle);
        }
        spawnedObstacles.Clear();
        lastSpawnZ = 0f;
        useFirstLane = true;

        // Respawn obstacles after resetting
    }
}
