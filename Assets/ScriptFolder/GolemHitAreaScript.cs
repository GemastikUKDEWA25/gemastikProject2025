using UnityEngine;

public class GolemHitAreaScript : MonoBehaviour
{
    public GolemScript golem;
    PlayerControllerSideViewScript player;
    public bool trigger = false;
    public float damage = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerSideViewScript>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (golem.parryAvailable)
            {
                if (player.isBlocking && player.parryTimer > 0)
                {
                    player.parryTimer = 0.3f;
                    golem.stunned();
                } 
            }

            if (trigger)
            {
                if (player.transform.position.x <= transform.position.x)
                {
                    player.knockFromRight = true;
                }
                if (player.transform.position.x > transform.position.x)
                {
                    player.knockFromRight = false;
                }
                player.attack(damage);
            }
        }
    }

    
}
