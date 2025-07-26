using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public bool continuous;
    public GameObject prefabToSpawn;  // Assign this in the Inspector
    public Transform spawnPoint;      // Optional: where to spawn from
    public float spawnInterval = 2f;  // Time between spawns
    private float timer = 0f;

    void Update()
    {
        if (continuous)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnPrefab(prefabToSpawn,spawnPoint);
                timer = 0f;
            }

            // Optional: spawn on key press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnPrefab(prefabToSpawn,spawnPoint);
            }
        }
    }

    public void SpawnPrefab(GameObject prefab,Transform spawnPostion)
    {
        Vector3 position = spawnPostion ? spawnPostion.position : transform.position;
        Instantiate(prefab, position, Quaternion.identity);
    }
}
