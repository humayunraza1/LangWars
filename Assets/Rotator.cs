using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its x-axis
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}