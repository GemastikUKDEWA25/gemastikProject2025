using System.Net;
using System.Threading;
using UnityEngine;

public class HearingAreaScript : MonoBehaviour
{
    public LineTesting lineOfSight;
    public PatrolEnemyScript enemy;
    float timer = 1f;
    bool isTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && !isTriggered)
        {
            lineOfSight.TriggerChase();
            isTriggered = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag + " Enemy Hear You");
        // lineOfSight.FollowPlayer(lineOfSight.playerTransform);

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        timer -= Time.deltaTime;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // if (collision.CompareTag("Player"))
        // {
        // }        
        isTriggered = false;
        timer = 1f;
    }
}
