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
        Debug.Log("TriggeredPlayer");
        if (collision.CompareTag("Player"))
        {
            if (trigger && !player.isSliding)
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
                player.knockBackCounter = player.knockBackTotalTime;
                Debug.Log("HitPlayer");
            }
        }
    }

    
}
