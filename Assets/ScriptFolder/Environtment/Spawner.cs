using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public ResetTrees resetTrees;
    void Start()
    {
        // Spawn once at the beginning
        // Instantiate(prefabToSpawn, transform.position, quaternion.identity);
    }

    // Optional: Spawn when you press a key
    void Update()
    {
        if (resetTrees.RespawnStatus)
        {
            Instantiate(prefabToSpawn, transform.position, quaternion.identity);
            resetTrees.changeRespawnStatus(false);
        }
    }
    

}
