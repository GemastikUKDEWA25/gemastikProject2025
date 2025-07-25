using UnityEngine;

public class TreesScript : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        move += Vector3.left;
        move = move.normalized;
        transform.position += move * 1f * Time.deltaTime;
    }
    

}
