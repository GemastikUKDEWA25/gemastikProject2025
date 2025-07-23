using UnityEngine;

public class IsGroundedScript : MonoBehaviour
{
    private bool isGrounded = false;
    public static IsGroundedScript Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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
        isGrounded = true;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    public void setGrounded(bool status)
    {
        isGrounded = status;
    }
    public bool getGrounded()
    {
        return isGrounded;
    }
    
}
