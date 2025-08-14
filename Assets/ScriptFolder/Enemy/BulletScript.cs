using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            if (collision.CompareTag("Player"))
            {
                PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
                player.attack(damage);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            if (collision.CompareTag("Player"))
            {
                PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
                player.attack(damage);
            }
            Destroy(gameObject);
        }
    }
    
}
