using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;  // Assign this in the Inspector
    public Transform spawnPoint;      // Optional: where to spawn from
    public float spawnInterval = 2f;  // Time between spawns
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }

        // Optional: spawn on key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        Vector3 position = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(prefabToSpawn, position, Quaternion.identity);
    }
}
