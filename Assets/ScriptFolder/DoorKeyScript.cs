using UnityEngine;

public class DoorKeyScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryScript playerInventory = collision.GetComponent<PlayerControllerScript>().inventory;
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInventory.carryObject(spriteRenderer.sprite, gameObject.tag);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryScript playerInventory = collision.GetComponent<PlayerControllerScript>().inventory;
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInventory.carryObject(spriteRenderer.sprite, gameObject.tag);
                Destroy(gameObject);
            }
        }
    }


}
