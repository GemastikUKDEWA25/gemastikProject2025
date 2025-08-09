using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScript : MonoBehaviour
{
    public SpriteRenderer InteractKey;
    public SpriteRenderer spriteRenderer;
    InventoryScript inventory;
    bool isInInteractArea = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;
        // spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && isInInteractArea)
            {
            if (inventory.Carry == "")
            {
                inventory.Carry = gameObject.tag;
                inventory.icon.sprite = spriteRenderer.sprite;
                inventory.icon.enabled = true;
                Destroy(gameObject);
            }
            
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            isInInteractArea = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
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
