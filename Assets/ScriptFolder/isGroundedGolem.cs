using UnityEngine;

public class isGroundedGolem : MonoBehaviour
{
    public GolemScript golem;
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
        if (collision.CompareTag("Floor") && !golem.isGrounded)
        {
            golem.isGrounded = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            golem.isGrounded = false;
        }
    }
}
