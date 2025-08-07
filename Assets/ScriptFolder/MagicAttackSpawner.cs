using UnityEngine;

public class MagicAttackSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Assign your prefab in the Inspector
    // public Transform spawnPoint;      // Optional spawn point (can be empty GameObject)

    public void spawnMagicDagger(string direction, Vector3 chargedSize, float chargedDamage)
    {
        GameObject spawned = Instantiate(objectToSpawn, transform.position, transform.rotation);

        MagicDaggerScript magicDagger = spawned.GetComponent<MagicDaggerScript>();
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(magicDagger.spawnSound);
        magicDagger.direction = direction;

        if (chargedSize != Vector3.zero && chargedDamage > 0)
        {
            spawned.transform.localScale = chargedSize;
            magicDagger.damage = chargedDamage;
        }
    }

}
