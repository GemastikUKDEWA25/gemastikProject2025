using UnityEngine;
using TMPro;

public class SavingTree : MonoBehaviour
{
    public TextMeshProUGUI InteractKey;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
            InteractKey.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.saving();
                Debug.Log("Saving");
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
            InteractKey.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.saving();
                Debug.Log("Saving");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractKey.enabled = false;            
        }
    }
}
