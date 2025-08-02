using UnityEngine;

public class OpenDoorRangeScript : MonoBehaviour
{
    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            if (playerInventory.Carry == "DoorKey")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerInventory.putDownCarriedObject();
                    Destroy(door);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryScript playerInventory = collision.GetComponent<PlayerControllerScript>().inventory;
            if (playerInventory.Carry == "DoorKey")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerInventory.putDownCarriedObject();
                    Destroy(door);
                }
            }
        }
    }
}
