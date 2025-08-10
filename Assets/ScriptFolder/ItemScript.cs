using UnityEngine;


public class ItemScript : MonoBehaviour
{
    public SpriteRenderer InteractKey;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    AudioSource audioSource;
    InventoryScript inventory;
    Animator animator;
    bool isInInteractArea = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractKey.enabled = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0) spriteRenderer.sprite = sprites[Mathf.FloorToInt(Random.Range(0, sprites.Length))];
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && isInInteractArea)
        {
            Debug.Log("picking up");
            // animator.Play("pickedUp");
            if (animator != null )animator.SetBool("isPickedUp",true);
            else pickUp();
            
        }
    }

    public void pickUp()
    {
        if (inventory.Carry == "")
        {
            inventory.Carry = gameObject.tag;
            inventory.icon.sprite = spriteRenderer.sprite;
            inventory.icon.enabled = true;
            Destroy(gameObject);
        }
    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
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
