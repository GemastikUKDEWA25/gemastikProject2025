using TMPro;
using UnityEngine;


public class TrashBinScript : MonoBehaviour
{
    public SpriteRenderer InteractKey;
    public SpriteRenderer TrashType;
    
    public AudioClip wrongSound;
    public AudioClip rightSound;
    public AudioSource audioSource;
    InventoryScript inventory;
    bool isInInteractArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;
        TrashType.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null)
        {
            if (inventory.Carry.ToLower() + "Bin".ToLower() == gameObject.tag.ToLower())
            {
                if (Input.GetKeyDown(KeyCode.E) && isInInteractArea)
                {
                    inventory.Carry = "";
                    inventory.icon.enabled = false;
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            TrashType.enabled = true;  
            isInInteractArea = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            TrashType.enabled = true;  
            isInInteractArea = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InteractKey.enabled = false;
            TrashType.enabled = false;  
            isInInteractArea = false;

        }
    }
}
