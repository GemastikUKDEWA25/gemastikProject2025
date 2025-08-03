using UnityEngine;

public class MagicAttackSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Assign your prefab in the Inspector
    public Transform spawnPoint;      // Optional spawn point (can be empty GameObject)

    public void spawnMagicDagger(string direction)
    {
        objectToSpawn.GetComponent<MagicDaggerScript>().direction = direction;
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
