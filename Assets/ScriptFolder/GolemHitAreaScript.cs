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
            if (trigger)
            {
                if (player.animator.GetBool("isBlocking"))
                {
                    if (golem.parryAvailable)
                    {
                        if (player.parryTimer > 0)
                        {
                            player.playSound(player.parrySound);
                            player.parryTimer = 0.3f;
                            golem.stunned();
                        }
                    }
                    if (!golem.parryAvailable && player.parryTimer <= 0)
                    {
                        player.playSound(player.blockedSound);
                    }
                }
                else
                {
                    if (player.transform.position.x <= transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    if (player.transform.position.x > transform.position.x)
                    {
                        player.knockFromRight = false;
                    }
                }
                player.attack(damage);
            }
        }
    }
}
