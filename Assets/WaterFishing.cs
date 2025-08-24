using UnityEngine;

public class WaterFishing : MonoBehaviour
{
    public GameObject interactUI; 
    public GameObject fishingMinigameUI;

    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactUI != null) interactUI.SetActive(false);
        if (fishingMinigameUI != null) fishingMinigameUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartFishing();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered water trigger");
            isPlayerNearby = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited water trigger");
            isPlayerNearby = false;
            interactUI.SetActive(false);
        }
    }

    void StartFishing()
    {
        fishingMinigameUI.SetActive(true); 
        interactUI.SetActive(false);

    }
}
