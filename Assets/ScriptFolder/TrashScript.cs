using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashScript : MonoBehaviour
{
    public TextMeshProUGUI InteractKey;
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        InteractKey.enabled = true;
        if (collision.CompareTag("Inventory"))
        {
            InventoryScript inventory = collision.GetComponent<InventoryScript>();
            Debug.Log("Inventory trigger");
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.Carry = gameObject.tag;
                inventory.icon.sprite = spriteRenderer.sprite;
                inventory.icon.enabled = true;
                if (inventory.Carry != "") Debug.Log("Carry"+gameObject.tag);
            
            }

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InventoryScript inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.Carry = gameObject.tag;
                inventory.icon.sprite = spriteRenderer.sprite;
                inventory.icon.enabled = true;
                if (inventory.Carry != "") Debug.Log("Carry"+gameObject.tag);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InventoryScript inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = false;
        }
    }
}
