using UnityEngine;
using System.Collections.Generic;

public class QuestionCubeSpawner : MonoBehaviour
{
    public GameObject questionCubePrefab; // Prefab of the question cube
    public Transform playerTransform; // Reference to the player's transform
    public int numberOfCubes = 2; // Number of question cubes to spawn
    public float firstCubeDistance = 80f; // Distance of the first cube from the player
    public float subsequentCubeDistance = 100f; // Distance between subsequent cubes
    public LayerMask groundLayer; // Layer mask to identify the ground layer

    private List<GameObject> spawnedCubes = new List<GameObject>();

    void Start()
    {
        SpawnQuestionCubes();
    }

    public void SpawnQuestionCubes()
    {
        ClearCubes(); // Clear existing cubes before spawning new ones

        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * firstCubeDistance;

        for (int i = 0; i < numberOfCubes; i++)
        {
            if (i == 0)
            {
                spawnPosition = playerTransform.position + playerTransform.forward * firstCubeDistance;
            }
            else
            {
                spawnPosition += playerTransform.forward * subsequentCubeDistance;
            }
            Debug.Log("Spawned Question Cube # " + i);
            // Adjust spawn position to ground level
            spawnPosition = GetGroundedPosition(spawnPosition);

            GameObject questionCube = Instantiate(questionCubePrefab, spawnPosition, Quaternion.identity);
            QuestionCube questionCubeComponent = questionCube.GetComponent<QuestionCube>();

            if (questionCubeComponent != null)
            {
                questionCubeComponent.questionIndex = i;
            }

            spawnedCubes.Add(questionCube);
        }
    }

    Vector3 GetGroundedPosition(Vector3 originalPosition)
    {
        RaycastHit hit;
        // Start the raycast from a higher position to ensure it hits the ground
        Vector3 raycastStart = originalPosition + Vector3.up * 50f;

        Debug.DrawRay(raycastStart, Vector3.down * 100f, Color.red, 5f); // Visualize the raycast in the scene

        if (Physics.Raycast(raycastStart, Vector3.down, out hit, 100f, groundLayer))
        {
            Debug.Log("Raycast hit at: " + hit.point);
            return new Vector3(originalPosition.x, hit.point.y, originalPosition.z);
        }
        else
        {
            Debug.LogWarning("Raycast did not hit the ground.");
        }

        return originalPosition; // Return original position if ground is not detected
    }

    public void ClearCubes()
    {
        foreach (var cube in spawnedCubes)
        {
            if (cube != null)
            {
                Destroy(cube);
            }
        }
        spawnedCubes.Clear();
    }
}
