using UnityEngine;

public class GolemHitAreaScript : MonoBehaviour
{
    public GolemScript golem;
    PlayerControllerSideViewScript player;
    public bool trigger = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerSideViewScript>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggeredPlayer");
        if (collision.CompareTag("Player"))
        {
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
                player.knockBackCounter = player.knockBackTotalTime;
                Debug.Log("HitPlayer");
            }
        }
    }

    
}
