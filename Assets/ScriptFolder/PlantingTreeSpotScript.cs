using UnityEngine;

public class PlantingTreeSpotScript : MonoBehaviour
{
    public SpriteRenderer InteractKey;
    public GameObject babyTree;
    public PrefabSpawner spawner;
    public AudioClip PlantingSound;
    public AudioSource audioSource;
    InventoryScript inventory;
    bool isInInteractArea;

    bool isPlanted = false;

    public ReforestationScript reforestationScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && !isPlanted)
        {
            if (inventory.Carry.ToLower() == "babytree")
            {
                if (Input.GetKeyDown(KeyCode.E) && isInInteractArea)
                {
                    reforestationScript.treesCount -= 1;
                    inventory.Carry = "";
                    inventory.icon.enabled = false;
                    isPlanted = true;
                    spawner.SpawnPrefab(babyTree, gameObject.transform);
                    Destroy(gameObject);
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory") && !isPlanted)
        {
            inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            isInInteractArea = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory") && !isPlanted)
        {
            inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            isInInteractArea = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InteractKey.enabled = false;
            isInInteractArea = false;

        }
    }
}
