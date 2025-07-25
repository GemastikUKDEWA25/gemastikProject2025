using UnityEngine;

public class ResetTrees : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool RespawnStatus = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeRespawnStatus(bool status)
    {
        RespawnStatus = status;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        RespawnStatus = true;
    }
}
