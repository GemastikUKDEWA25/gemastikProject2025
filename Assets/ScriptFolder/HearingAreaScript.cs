using System.Net;
using System.Threading;
using UnityEngine;

public class HearingAreaScript : MonoBehaviour
{
    public LineTesting lineOfSight;
    public PatrolEnemyScript enemy;
    float timer = 1f;
    Vector3 lastPlayerPostion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            enemy.setPlayerSeen(true); 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag + " Enemy Hear You");
        if (collision.CompareTag("Player"))
        {
            lastPlayerPostion = collision.transform.position;
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) timer -= Time.deltaTime;
        }
    }
}
