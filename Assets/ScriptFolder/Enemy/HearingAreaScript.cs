using UnityEngine;

public class HearingAreaScript : MonoBehaviour
{
    public LineTesting lineOfSight;
    public PatrolEnemyScript enemy;

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
        // Debug.Log(collision.tag + " Enemy Hear You");
        lineOfSight.TriggerChase();

    }
    void OnTriggerStay2D(Collider2D collision)
    {

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
