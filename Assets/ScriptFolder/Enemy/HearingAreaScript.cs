using UnityEngine;

public class HearingAreaScript : MonoBehaviour
{
    public LineTesting lineOfSight;
    public PatrolEnemyScript enemy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        lineOfSight.TriggerChase();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
