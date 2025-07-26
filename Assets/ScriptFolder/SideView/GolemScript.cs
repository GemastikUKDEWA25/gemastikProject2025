using UnityEngine;
using TMPro;

public class GolemScript : MonoBehaviour
{
    float health = 100;
    public static EnemyScript Instance { get; private set; }
    public TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthText.text = health.ToString();

    }

    public void attack(float damage)
    {
        health -= damage;
    }
    public float getHealth()
    {
        return health;
    }

    public void chasePlayer(Rigidbody2D rb, PlayerControllerSideViewScript player,float movementSpeed)
    {
        float direction = player.transform.position.x - transform.position.x;

        // Only move if far enough (optional threshold)
        if (Mathf.Abs(direction) > 0.01f)
        {
            float move = Mathf.Sign(direction) * movementSpeed * Time.fixedDeltaTime;
            Vector2 newPos = new Vector2(transform.position.x + move, transform.position.y);
            rb.MovePosition(newPos);

            // Optional: Flip to face direction
            transform.localScale = new Vector3(Mathf.Sign(direction), 1f, 1f);
        }
    }
}
