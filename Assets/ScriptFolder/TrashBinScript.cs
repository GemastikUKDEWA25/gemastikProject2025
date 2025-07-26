using TMPro;
using UnityEngine;


public class TrashBinScript : MonoBehaviour
{
    public TextMeshProUGUI InteractKey;
    public AudioClip wrongSound;
    public AudioClip rightSound;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InventoryScript inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            Debug.Log(inventory.Carry.ToLower() + "Bin".ToLower());
            Debug.Log(gameObject.tag.ToLower());

            if (inventory.Carry.ToLower() + "Bin".ToLower() == gameObject.tag.ToLower())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.Carry = "";
                    inventory.icon.enabled = false;
                }
            }

        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            InventoryScript inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = true;
            Debug.Log(inventory.Carry.ToLower() + "Bin".ToLower());
            Debug.Log(gameObject.tag.ToLower());

            if (inventory.Carry.ToLower() + "Bin".ToLower() == gameObject.tag.ToLower())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.Carry = "";
                    inventory.icon.enabled = false;
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inventory"))
        {
            // InventoryScript inventory = collision.GetComponent<InventoryScript>();
            InteractKey.enabled = false;

        }
    }
}
